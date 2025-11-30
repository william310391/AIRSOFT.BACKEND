INSERT INTO Ubigeo(UbigeoCodigo
,DepartamentoID
,ProvinciaID
,DistritoID
,DepartamentoNombre
,ProvinciaNombre
,DistritoNombre
,NombreCapital
,RegionNaturalID
,RegionNaturalNombre)


SELECT column1 AS ubigeoid,
  cast(LEFT(column1, 2) AS int) AS departamentoid ,
 cast(left(RIGHT(column1, 4), 2) AS int) AS provinciaid,
 cast(RIGHT(column1, 2)AS int) AS distritoid,
column2 AS Departamento,
ud.Column3 AS provincia,
ud.Column4 AS distrito,
ud.Column5 AS nombreCapital,
ud.Column6 AS RegionNaturalID,
ud.Column7 AS RegionalNatural

 FROM [UBIGEO 2022_1891 distritos] ud