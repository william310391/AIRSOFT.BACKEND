using Airsoft.Application.DTOs;
using Airsoft.Application.DTOs.Mensaje;
using Airsoft.Application.Interfaces;
using Airsoft.Domain.Entities;
using Airsoft.Domain.Enum;
using Airsoft.Infrastructure.Helpers;
using Airsoft.Infrastructure.Intefaces;
using AutoMapper;

namespace Airsoft.Application.Services
{
    public class MensajeService: IMensajeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly RedisCacheHelper _redisCache;

        public MensajeService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, RedisCacheHelper redisCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _redisCache = redisCache;
        }

        public async Task<ApiResponse<List<MensajeResponse>>> GetMensajesByChatID(GetMensajeRequest request)
        {
            var key = $"chat_{request.chatID}";
            var dataDTO = await _redisCache.GetAsync<List<MensajeResponse>>(key);
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var cantidad = dataDTO?.Count ?? 0;

            // Consultar BD solo si:
            // 1. No existe cache
            // 2. Cache vacío
            // 3. Se solicita más de lo almacenado
            if (dataDTO == null || !dataDTO.Any() || request.pageSize > cantidad)
            {
                var data = await _unitOfWork.MensajeRepository.GetMensaje(
                    request.pageSize,
                    request.chatID
                );

                dataDTO = _mapper.Map<List<MensajeResponse>>(data)
                    .Select(x =>
                    {
                        x.esRemitente = usuarioID == x.usuarioEnvioID;
                        return x;
                    })
                    .OrderBy(x => x.fecha)
                    .ToList();

        
                if (dataDTO.Any())
                {
                    await _redisCache.SetAsync(key, dataDTO);
                }
            }
            else
            {
                // Obtener solo la cantidad requerida desde Redis
                dataDTO = dataDTO
                    .OrderByDescending(x => x.fecha)
                    .Take(request.pageSize)
                    .OrderBy(x => x.fecha)
                    .ToList();
            }

            return new ApiResponse<List<MensajeResponse>>
            {
                Success = true,
                Message = dataDTO.Any()
                    ? "Mensajes obtenidos"
                    : "No hay mensajes",
                Data = dataDTO,
            };
        }

        public async Task<ApiResponse<MensajeResponse>> Save(MensajeSaveRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var mensaje = _mapper.Map<Mensaje>(request);

            mensaje.MensajeID = Guid.NewGuid();
            mensaje.UsuarioEnvioID = usuarioID;
            mensaje.Fecha = DateTime.Now;

            var res = await _unitOfWork.MensajeRepository.Save(mensaje);

            if (res){
                await ActualizarMensajesRedis(new MensajeRequest
                {
                    chatID = request.chatID,
                    mensajeID = mensaje.MensajeID,
                    usuarioEnvioID = mensaje.UsuarioEnvioID,
                    fecha = mensaje.Fecha,
                    contenido = mensaje.Contenido

                }, EnumMensaje.Save);
            }

            var mensajeDTO = _mapper.Map<MensajeResponse>(mensaje);
            mensajeDTO.esRemitente = true;

            return new ApiResponse<MensajeResponse>
            {
                Success = res,
                Message = res ? "Mensaje enviado exitosamente" : "Error al enviar el mensaje",
                Data = mensajeDTO
            };
        }

        public async Task<ApiResponse<bool>> Update(MensajeUpdateRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var mensaje = _mapper.Map<Mensaje>(request);
            mensaje.UsuarioEnvioID = usuarioID;
            mensaje.Fecha = DateTime.Now;

            var res = await _unitOfWork.MensajeRepository.Update(mensaje);

            if (res)
            {
                await ActualizarMensajesRedis(new MensajeRequest
                {
                    chatID = request.chatID,
                    mensajeID = mensaje.MensajeID,
                    usuarioEnvioID = mensaje.UsuarioEnvioID,
                    fecha = mensaje.Fecha,
                    contenido = mensaje.Contenido
                }, EnumMensaje.Update);
            }

            return new ApiResponse<bool>
            {
                Success = res,
                Message = res ? "Mensaje actualizado exitosamente" : "Error al actualizar el mensaje",
                Data = res
            };
        }

        public async Task<ApiResponse<bool>> Delete(MensajeDeleteRequest request)
        {
            var usuarioID = _userContextService.GetAttribute<int>(EnumClaims.UsuarioID);
            var mensaje = _mapper.Map<Mensaje>(request);

            mensaje.UsuarioEnvioID = usuarioID;
            mensaje.Fecha = DateTime.Now;

            var res = await _unitOfWork.MensajeRepository.Delete(mensaje);

            if (res)
            {
                await ActualizarMensajesRedis(new MensajeRequest
                {
                    chatID = request.chatID,
                    mensajeID = mensaje.MensajeID,
                    usuarioEnvioID = mensaje.UsuarioEnvioID,
                    fecha = mensaje.Fecha,
                    contenido = mensaje.Contenido
                }, EnumMensaje.Delete);
            }

            return new ApiResponse<bool>
            {
                Success = res,
                Message = res ? "Mensaje eliminado exitosamente" : "Error al eliminar el mensaje",
                Data = res
            };
        }

        public async Task<ApiResponse<bool>> UpdateUnread(Guid chatID, int usuarioID)
        {
            var res = await _unitOfWork.ChatRepository.UpdateUnread(chatID, usuarioID);

            return new ApiResponse<bool>
            {
                Success = res,
                Message = res ? "Mensajes eliminados exitosamente" : "Error al eliminar los mensajes",
                Data = res
            };
        }

        public async Task<bool> ActualizarMensajesRedis(MensajeRequest request, int enumMensaje)
        {
            var key = $"chat_{request.chatID}";
            var result = false;

            // Obtener mensajes desde Redis
            var data = await _redisCache.GetAsync<List<MensajeResponse>>(key);

            // Si no existe cache, cargar últimos mensajes desde BD
            if (data == null || !data.Any())
            {
                var res = await GetMensajesByChatID(new GetMensajeRequest
                {
                    chatID = request.chatID,
                    pageSize = 100
                });

                data = res.Data?.ToList() ?? new List<MensajeResponse>();

                await _redisCache.SetAsync(key, data);
            }

            switch (enumMensaje)
            {
                case EnumMensaje.Save:

                    data.Add(new MensajeResponse
                    {
                        chatID = request.chatID,
                        mensajeID = request.mensajeID,
                        usuarioEnvioID = request.usuarioEnvioID,
                        fecha = request.fecha,
                        contenido = request.contenido,
                        esRemitente = true
                    });

                    // Mantener máximo 10 mensajes recientes
                    data = data
                        .OrderByDescending(x => x.fecha)
                        .Take(10)
                        .OrderBy(x => x.fecha)
                        .ToList();

                    await _redisCache.SetAsync(key, data);
                    result = true;
                    break;

                case EnumMensaje.Update:

                    var mensajeToUpdate = data.FirstOrDefault(m => m.mensajeID == request.mensajeID);

                    if (mensajeToUpdate != null)
                    {
                        mensajeToUpdate.contenido = request.contenido;
                        mensajeToUpdate.fecha = request.fecha;

                        await _redisCache.SetAsync(key, data);
                        result = true;
                    }

                    break;

                case EnumMensaje.Delete:

                    var mensajeToDelete = data.FirstOrDefault(m => m.mensajeID == request.mensajeID);

                    if (mensajeToDelete != null)
                    {
                        data.Remove(mensajeToDelete);

                        await _redisCache.SetAsync(key, data);
                        result = true;
                    }

                    break;
            }

            return result;
        }
    }
}
