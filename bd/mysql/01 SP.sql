delimiter $$
create procedure altaInforme(unaFecha date, unTipoMedicion tinyint unsigned,out idGenerado int )
begin 
	declare mx float default 0;
	declare mn float default 0;
	declare pr float default 0;

	select max(valor) into mx,min(valor) into mn,average(valor) into pr
	from Informe i,tipoMedicion tm,Medicion M
	where m.idTipoMedicion = tm.idTipoMedicion
	and tm.idTipoMedicion = m.idTipoMedicion
	and unaFecha = i.fecha
	and unTipoMedicion = i.tipoMedicion;

	insert into Informe(fecha,idTipoMedicion,minimo,maximo,promedio)
		values (unaFecha,unTipoMedicion,mn,mx,pr);

	select last_insert_id() into idGenerado;
end$$