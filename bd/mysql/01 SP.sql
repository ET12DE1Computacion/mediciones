delimiter $$
create procedure altaInforme(unaFecha date, unTipoMedicion tinyint unsigned,out idGenerado int )
begin 
	declare mx float default 0;
	declare mn float default 0;
	declare pr float default 0;

	select max(valor), min(valor), avg(valor) into mx, mn, pr
	from Informe i,tipoMedicion tm,Medicion M
	where m.idTipoMedicion = tm.idTipoMedicion
	and tm.idTipoMedicion = m.idTipoMedicion
	and unaFecha = i.fecha
	and unTipoMedicion = i.tipoMedicion;

	insert into Informe(fecha,idTipoMedicion,minimo,maximo,promedio)
		values (unaFecha,unTipoMedicion,mn,mx,pr);

	select last_insert_id() into idGenerado;
end $$

create procedure AltaTipoMedicion(in unTipoMedicion varchar (45), out idGenerado tinyint unsigned)
begin
	insert into TipoMedicion(TipoMedicion)
					values	(unTipoMedicion);
    select last_insert_id() into idGenerado;
end $$

 
create procedure altaMedicion(in unIdTipoMedicion tinyint unsigned, in unValor float, in unaFechaHora datetime, out idGenerado int unsigned)
begin
	insert into medicion(idTipoMedicion, valor, fechaHora)
    values			 (unidTipoMedicion, unValor, unaFechaHora);
    select last_insert_id() into idGenerado;
end $$