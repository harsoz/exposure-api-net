CREATE PROCEDURE [shp].[procCreateShape]
	@name varchar(500),
	@params  shp.tpParams readonly,
	@binary varchar(max)
AS
begin try
	if(Isnull(@name,'') = '')
		begin
			raiserror('name is mandatory',16,1)
		end

	if not exists(select * from @params)
		begin
			raiserror('params are mandatory',16,1)
		end

	if(Isnull(cast(@binary as varchar(max)),'') = '')
		begin
			raiserror('binary data is mandatory',16,1)
		end

	insert into tbShapes([name], [binary])
	select @name, @binary

	declare @id int = SCOPE_IDENTITY()

	insert into tbShapeParams([order], [name], [value], ShapeId)
	select [order], [name], [value], @id from @params

	select 'Ok' [Status], 'Registered successfully' [Message]

end try
begin catch
	select 'Error'[Status], ERROR_MESSAGE() [Message]
end catch



