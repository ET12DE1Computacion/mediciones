-- MySQL Script generated by MySQL Workbench
-- Wed Oct 10 17:27:39 2018
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mediciones
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `mediciones` ;

-- -----------------------------------------------------
-- Schema mediciones
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mediciones` DEFAULT CHARACTER SET utf8 ;
USE `mediciones` ;

-- -----------------------------------------------------
-- Table `mediciones`.`TipoMedicion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mediciones`.`TipoMedicion` (
  `idTipoMedicion` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `tipoMedicion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idTipoMedicion`),
  UNIQUE INDEX `tipoMedicion_UNIQUE` (`tipoMedicion` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mediciones`.`Medicion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mediciones`.`Medicion` (
  `idMedicion` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `idTipoMedicion` TINYINT UNSIGNED NOT NULL,
  `valor` FLOAT NOT NULL,
  `fechaHora` DATETIME NOT NULL,
  PRIMARY KEY (`idMedicion`),
  INDEX `fk_Medicion_TipoMedicion_idx` (`idTipoMedicion` ASC),
  CONSTRAINT `fk_Medicion_TipoMedicion`
    FOREIGN KEY (`idTipoMedicion`)
    REFERENCES `mediciones`.`TipoMedicion` (`idTipoMedicion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mediciones`.`Informe`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mediciones`.`Informe` (
  `idInforme` INT NOT NULL AUTO_INCREMENT,
  `idTipoMedicion` TINYINT UNSIGNED NOT NULL,
  `fecha` DATE NOT NULL,
  `minimo` FLOAT NOT NULL,
  `maximo` FLOAT NOT NULL,
  `promedio` FLOAT NULL,
  PRIMARY KEY (`idInforme`),
  INDEX `fk_Informe_TipoMedicion1_idx` (`idTipoMedicion` ASC),
  CONSTRAINT `fk_Informe_TipoMedicion1`
    FOREIGN KEY (`idTipoMedicion`)
    REFERENCES `mediciones`.`TipoMedicion` (`idTipoMedicion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

CREATE USER IF NOT EXISTS `mediciones`@`localhost` IDENTIFIED BY 'mediciones';
GRANT ALL ON mediciones.* to `mediciones`@`localhost`;