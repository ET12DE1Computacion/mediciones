CREATE DEFINER=`root`@`localhost` PROCEDURE `PopulateMedicion`()
begin
	declare tiempo datetime;
	declare intervalo int;
    
    set tiempo = now();
    set intervalo = 0;

	while intervalo < 10000 do
		insert into medicion (idTipoMedicion, valor, fechaHora) values (1, rand() * 100, date_add(tiempo, interval intervalo second));
		set intervalo = intervalo + 1;
	end while;
end ;;