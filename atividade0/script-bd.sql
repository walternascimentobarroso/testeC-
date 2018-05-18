CREATE DATABASE oficina;

CREATE TABLE especialidade (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  descricao VARCHAR(255) 
);

CREATE TABLE subservico (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  descricao VARCHAR(255),
  especialidadeid INT, 
  FOREIGN KEY (especialidadeid) REFERENCES especialidade(id)
);

CREATE TABLE maodeobra (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  codigo VARCHAR(55),
  descricao VARCHAR(255),
  precohora VARCHAR(55)
);

CREATE TABLE funcionario (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  nome VARCHAR(255),
  maodeobraid INT, 
  FOREIGN KEY (maodeobraid) REFERENCES maodeobra(id)
);

CREATE TABLE cliente (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  nome VARCHAR(255),
  telefone VARCHAR(255),
  celular VARCHAR(255)
);

CREATE TABLE desconto (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  precototal VARCHAR(255),
  desconto VARCHAR(255)
);

CREATE TABLE ordemdeservico (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  descricao VARCHAR(255),
  clienteid INT, FOREIGN KEY (clienteid) REFERENCES cliente(id),
  funcionarioid INT, FOREIGN KEY (funcionarioid) REFERENCES funcionario(id),
  subservicoid INT, FOREIGN KEY (subservicoid) REFERENCES subservico(id),
  descontoid INT, FOREIGN KEY (descontoid) REFERENCES desconto(id),
  observacao VARCHAR(255)
);