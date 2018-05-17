CREATE DATABASE oficina;

CREATE TABLE especialidade (
  `id` INT(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `descricao` VARCHAR(255) 
);

subservico
 id
 descricao
 especialidadeId

mao de obra
 id
 codigo
 descricao
 preco-hora

funcionario
 id
 nome
 maodeobraid

cliente
 id
 nome
 telefone
 celular

ordemdeservico
 id
 descricao
 clienteId
 funcionarioId
 subservicoId
 descontoId
 observacao

desconto
 id
 preco_total
 desconto
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema estoque
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `estoque` ;

-- -----------------------------------------------------
-- Schema estoque
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `estoque` DEFAULT CHARACTER SET utf8 ;
USE `estoque` ;

-- -----------------------------------------------------
-- Table `estoque`.`categoria`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `estoque`.`categoria` ;

CREATE TABLE IF NOT EXISTS `estoque`.`categoria` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8
COMMENT = '		';


-- -----------------------------------------------------
-- Table `estoque`.`produto`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `estoque`.`produto` ;

CREATE TABLE IF NOT EXISTS `estoque`.`produto` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `descricao` LONGTEXT NOT NULL,
  `preco_custo` DECIMAL(9,2) NOT NULL,
  `preco_venda` DECIMAL(9,2) NOT NULL,
  `quantidade` DOUBLE NOT NULL,
  `unidade_medida` CHAR(2) NULL DEFAULT NULL,
  `categoria_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`, `categoria_id`),
  INDEX `fk_produto_categoria_idx` (`categoria_id` ASC),
  CONSTRAINT `fk_produto_categoria`
    FOREIGN KEY (`categoria_id`)
    REFERENCES `estoque`.`categoria` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 12
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
