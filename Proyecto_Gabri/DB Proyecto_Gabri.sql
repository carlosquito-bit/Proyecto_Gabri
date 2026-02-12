CREATE DATABASE IF NOT EXISTS hotel_reservas;
USE hotel_reservas;

-- Tabla: Usuarios (Huéspedes)
CREATE TABLE usuarios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(120) NOT NULL UNIQUE,
    telefono VARCHAR(20),
    direccion VARCHAR(255),
    ciudad VARCHAR(100),
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    activo BOOLEAN DEFAULT TRUE
);

-- Tabla: Tipos de Habitación
CREATE TABLE tipos_habitacion (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    capacidad INT NOT NULL,
    precio_noche DECIMAL(10, 2) NOT NULL,
    amenidades TEXT,
    activo BOOLEAN DEFAULT TRUE
);

-- Tabla: Habitaciones
CREATE TABLE habitaciones (
    id INT PRIMARY KEY AUTO_INCREMENT,
    numero VARCHAR(10) NOT NULL UNIQUE,
    piso INT NOT NULL,
    tipo_habitacion_id INT NOT NULL,
    estado ENUM('disponible', 'ocupada', 'mantenimiento', 'limpieza') DEFAULT 'disponible',
    fecha_ultima_limpieza DATE,
    observaciones TEXT,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (tipo_habitacion_id) REFERENCES tipos_habitacion(id)
);

-- Tabla: Reservas
CREATE TABLE reservas (
    id INT PRIMARY KEY AUTO_INCREMENT,
    usuario_id INT NOT NULL,
    habitacion_id INT NOT NULL,
    fecha_entrada DATE NOT NULL,
    fecha_salida DATE NOT NULL,
    fecha_reserva TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    numero_huespedes INT NOT NULL,
    estado ENUM('confirmada', 'pendiente', 'cancelada', 'completada') DEFAULT 'confirmada',
    notas TEXT,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    FOREIGN KEY (habitacion_id) REFERENCES habitaciones(id),
    CONSTRAINT check_fechas CHECK (fecha_salida > fecha_entrada)
);

-- Tabla: Servicios Adicionales
CREATE TABLE servicios_adicionales (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10, 2) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

-- Tabla: Servicios en Reservas (Relación muchos a muchos)
CREATE TABLE reserva_servicios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    reserva_id INT NOT NULL,
    servicio_id INT NOT NULL,
    cantidad INT DEFAULT 1,
    fecha_servicio DATE,
    FOREIGN KEY (reserva_id) REFERENCES reservas(id) ON DELETE CASCADE,
    FOREIGN KEY (servicio_id) REFERENCES servicios_adicionales(id)
);

-- Tabla: Pagos
CREATE TABLE pagos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    reserva_id INT NOT NULL,
    monto DECIMAL(10, 2) NOT NULL,
    metodo_pago ENUM('tarjeta_credito', 'tarjeta_debito', 'transferencia', 'efectivo') NOT NULL,
    fecha_pago TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    estado_pago ENUM('completado', 'pendiente', 'reembolso') DEFAULT 'completado',
    numero_transaccion VARCHAR(100),
    FOREIGN KEY (reserva_id) REFERENCES reservas(id)
);

-- Tabla: Disponibilidad Diaria
CREATE TABLE disponibilidad (
    id INT PRIMARY KEY AUTO_INCREMENT,
    habitacion_id INT NOT NULL,
    fecha DATE NOT NULL,
    disponible BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (habitacion_id) REFERENCES habitaciones(id),
    UNIQUE KEY unique_habitacion_fecha (habitacion_id, fecha)
);

-- Tabla: Auditoría de Cambios
CREATE TABLE auditoria (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tabla VARCHAR(100) NOT NULL,
    id_registro INT NOT NULL,
    accion ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    datos_anteriores JSON,
    datos_nuevos JSON,
    usuario VARCHAR(100),
    fecha_cambio TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);