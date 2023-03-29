CREATE TABLE [shp].[tbShapeParams]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[ShapeId] int,
	[Order] int,
	[Name] varchar(500),
	[Value] decimal(10,2),
	FOREIGN KEY (ShapeId) REFERENCES shp.tbShapes(Id)
)
