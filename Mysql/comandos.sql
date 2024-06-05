create database estoque;

use estoque;

create table produto (
id int not null auto_increment,
nome varchar(255),
quantidade int,
primary key (id)
)default charset = utf8;

create table cliente (
id int not null auto_increment,
nome varchar(255),
primary key (id)
)default charset = utf8;

CREATE TABLE SaidaProduto (
    id INT NOT NULL AUTO_INCREMENT,
    idProduto INT NOT NULL,
    idCliente INT NOT NULL,
    DataHora DATETIME DEFAULT CURRENT_TIMESTAMP,
    quantidade INT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (idProduto) REFERENCES produto(id),
    FOREIGN KEY (idCliente) REFERENCES cliente(id)
) DEFAULT CHARSET=utf8;

ALTER TABLE saidaproduto
ADD COLUMN NomeProduto varchar(255);

ALTER TABLE saidaproduto
ADD COLUMN NomeCliente varchar(255);

select * from produto;
select * from cliente;
select * from saidaproduto;

SELECT sp.id, sp.idProduto, sp.idCliente, sp.DataHora, sp.quantidade, p.nome AS nome_produto, c.nome AS nome_cliente
FROM SaidaProduto sp
INNER JOIN Produto p ON sp.idProduto = p.id
INNER JOIN Cliente c ON sp.idCliente = c.id;

SELECT sp.id ,p.nome AS idProduto, c.nome AS idCliente, sp.DataHora, sp.quantidade
FROM SaidaProduto sp
INNER JOIN Produto p ON sp.idProduto = p.id
INNER JOIN Cliente c ON sp.idCliente = c.id;

DELIMITER //

CREATE TRIGGER atualizar_quantidade_produto AFTER INSERT ON SaidaProduto
FOR EACH ROW
BEGIN
    UPDATE produto
    SET quantidade = quantidade - NEW.quantidade
    WHERE id = NEW.idProduto;
END;
//

DELIMITER ;

DELIMITER //

CREATE TRIGGER devolver_quantidade_produto AFTER DELETE ON SaidaProduto
FOR EACH ROW
BEGIN
    UPDATE produto
    SET quantidade = quantidade + OLD.quantidade
    WHERE id = OLD.idProduto;
END;
//

DELIMITER ;
//

DELIMITER ;