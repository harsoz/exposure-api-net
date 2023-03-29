CREATE PROCEDURE [shp].[procGetShapes]
AS
select
 m.Id,
 m.Name,
 m.Binary,
(select 
	p.Id,
	p.[Name],
	p.[Order],
	p.Value
	from shp.tbShapeParams p
	where p.ShapeId = m.Id
	for json path
	) Params
from shp.tbShapes m
for json path