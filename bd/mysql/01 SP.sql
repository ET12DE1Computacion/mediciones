delimiter // 
create procedure AltaTipoMedicion(in unTipoMedicion varchar (45), out idGenerado tinyint unsigned)
begin
	insert into TipoMedicion(TipoMedicion)
					values	(unTipoMedicion);
    select last_insert_id() into idGenerado;
end //