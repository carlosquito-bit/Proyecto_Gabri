USE sakila;
DROP PROCEDURE if exists AnnadirActor;
DELIMITER //
CREATE PROCEDURE AnnadirActor(IN _nombre VARCHAR(45),
							  IN _apellido VARCHAR(45))
BEGIN


	INSERT INTO actor(first_name,last_name)
				VALUES(_nombre, _apellido);

END//
DELIMITER ;

-- SELECT * FROM actor;

DROP PROCEDURE if exists DameCatalogo;
DELIMITER //
CREATE PROCEDURE DameCatalogo()
BEGIN

	SELECT title, description
    FROM film;
	
END//
DELIMITER ;

Call DameCatalogo();

DROP PROCEDURE if exists ContarPeliculas;
DELIMITER //
CREATE PROCEDURE ContarPeliculas(OUT _res INT)
BEGIN

	SELECT COUNT(*)
    FROM film
    INTO _res;
	
END//
DELIMITER ;

DROP PROCEDURE if exists Login;
DELIMITER //
CREATE PROCEDURE Login(IN _user VARCHAR(16),
					   IN _pass VARCHAR(40),
						OUT _id_empleadoLoged INT)
BEGIN
	
    SET _id_empleadoLoged = -1;
    
    SELECT staff_id FROM staff -- CASO <id_empleado> si Login correcto
		WHERE username LIKE _user and password LIKE _pass
        INTO _id_empleadoLoged;

END//
DELIMITER ;

DROP PROCEDURE if exists BuscarIdPorTitulo;
DELIMITER //
CREATE PROCEDURE BuscarIdPorTitulo(IN _titulo VARCHAR(128),
									OUT _id INT)
BEGIN
	
    SET _id = NULL;
    
	SELECT film_id FROM film
		WHERE title LIKE _titulo
        INTO _id;
	
    IF(_id IS NULL) THEN
		SET _id = -1; -- Pelicula no encontrada
	END IF;	

END//
DELIMITER ;

DROP PROCEDURE if exists DameIdObjetoInventarioLibre;
DELIMITER //
CREATE PROCEDURE DameIdObjetoInventarioLibre(IN _film_id INT,
											 OUT _id_inventario INT)
BEGIN
	DECLARE n_disponibles INT;
    SET n_disponibles = 0;
    -- COMPROBAR QUE HAY OBJETO LIBRE DE INVENTARIO PARA ESA PELICULA
    Call NPeliculasDisponibles(_film_id, n_disponibles);
    
    IF(n_disponibles < 1) THEN
		SET _id_inventario = 0;
	ELSE
		SELECT i.inventory_id INTO _id_inventario
		FROM inventory i
			LEFT JOIN rental r
				ON i.inventory_id = r.inventory_id
				AND r.return_date IS NULL
			WHERE r.rental_id IS NULL and i.film_id = _film_id
			ORDER BY i.film_id
			LIMIT 1;
		IF(_id_inventario IS NULL) THEN
			SET _id_inventario = -1;
		END IF;
    END IF;
END//
DELIMITER ;

DROP PROCEDURE if exists DameObjetosAlquilados;
DELIMITER //
CREATE PROCEDURE DameObjetosAlquilados(IN _id_cliente INT)
BEGIN	
    SELECT r.rental_id AS IdAlquiler,
    f.title AS Titulo,
    r.rental_date AS FechaAlquiler,
    DATEDIFF(SYSDATE(),r.rental_date) AS DiasAlquilado
    FROM rental r JOIN inventory i
    USING(inventory_id)
    JOIN film f
    USING(film_id)
    WHERE r.return_date IS NULL and r.customer_id = _id_cliente;
    
END//
DELIMITER ;



DROP PROCEDURE if exists DevolverObjeto;
DELIMITER //
CREATE PROCEDURE DevolverObjeto(IN _id_alquiler INT)
BEGIN
    UPDATE rental SET return_date = SYSDATE()
		WHERE rental_id = _id_alquiler;
END//
DELIMITER ;