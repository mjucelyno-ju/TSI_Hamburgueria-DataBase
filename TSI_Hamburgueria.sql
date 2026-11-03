
-- codigos utilizandos na criaçao das tabelas do banco de dados da Hamburgueria:

CREATE TABLE clientes (
    id_clientes INT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    telefone VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE pedidos (
    id_pedidos INT PRIMARY KEY,
    id_clientes INT NOT NULL,
    status VARCHAR(30) NOT NULL
        CHECK (status IN ('Pendente', 'Em preparo', 'Saiu para entrega', 'Entregue', 'Cancelado')),
    data_pedidos TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    tipo_de_entrega VARCHAR(30) NOT NULL
        CHECK (tipo_de_entrega IN ('Entrega', 'Retirada')),
    CONSTRAINT fk_pedidos_clientes
        FOREIGN KEY (id_clientes) REFERENCES clientes(id_clientes)
);

CREATE TABLE itens_do_pedido (
    id_item INT PRIMARY KEY,
    id_pedidos INT NOT NULL,
    id_produtos INT NOT NULL,
    quantidade INT NOT NULL
        CHECK (quantidade > 0),
    preco_unitario DECIMAL(10,2) NOT NULL
        CHECK (preco_unitario >= 0),
    FOREIGN KEY (id_pedidos) REFERENCES pedidos(id_pedidos),
    FOREIGN KEY (id_produtos) REFERENCES produtos(id_produtos)
);

CREATE TABLE produtos (
    id_produtos INT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    preco DECIMAL(10,2) NOT NULL
        CHECK (preco >= 0),
    categoria VARCHAR(50) NOT NULL,
    disponivel BOOLEAN NOT NULL DEFAULT 1
        CHECK (disponivel IN (0, 1))
);

CREATE TABLE produto_ingrediente (
    id_produtos INT NOT NULL,
    id_ingredientes INT NOT NULL,
    quantidade_utilizada INT NOT NULL
        CHECK (quantidade_utilizada > 0),
    PRIMARY KEY (id_produtos, id_ingredientes),
    FOREIGN KEY (id_produtos) REFERENCES produtos(id_produtos),
    FOREIGN KEY (id_ingredientes) REFERENCES ingredientes(id_ingredientes)
);

CREATE TABLE ingredientes (
    id_ingredientes INT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL UNIQUE,
    quantidade_estoque INT NOT NULL DEFAULT 0
        CHECK (quantidade_estoque >= 0),
    validade DATE
);

CREATE TABLE pagamentos (
    id_pagamentos INT PRIMARY KEY,
    id_pedidos INT NOT NULL,
    forma_pagamento VARCHAR(30) NOT NULL
        CHECK (forma_pagamento IN ('Dinheiro', 'Pix', 'Cartão de crédito', 'Cartão de débito')),
    status_pagamento VARCHAR(30) NOT NULL DEFAULT 'Pendente'
        CHECK (status_pagamento IN ('Pendente', 'Pago', 'Recusado', 'Estornado')),
    data_pagamento DATETIME DEFAULT CURRENT_TIMESTAMP,
    valor_pago DECIMAL(10,2) NOT NULL
        CHECK (valor_pago > 0),
    FOREIGN KEY (id_pedidos) REFERENCES pedidos(id_pedidos)
);
--codigo utilizado na criaçao do views pedidos_com_total,
-- que calcula o valor total de cada pedido com base nos itens do pedido e seus preços unitários:

CREATE VIEW pedidos_com_total AS
SELECT p.id_pedidos, p.id_clientes, p.status, p.data_pedidos, p.tipo_de_entrega,
       ROUND(COALESCE(SUM(i.quantidade * i.preco_unitario), 0), 2) AS valor_total
FROM pedidos p
LEFT JOIN itens_do_pedido i ON i.id_pedidos = p.id_pedidos
GROUP BY p.id_pedidos;

--codigos utilizados para inserir  os dados nas tabelas:
INSERT INTO
    clientes (id_clientes, nome, telefone)
VALUES
    (1, 'augusto', '(84) 99699-9967'),
    (2, 'jose', '(84) 94699-5567'),
    (3, 'maria', '(84) 94680-3517'),
    (4, 'fernando', '(84) 97699-7767'),
    (5, 'geovana', '(84) 93699-3567'),
    (6, 'pedro', '(84) 94680-2515'),
    (7, 'carlos', '(84) 97987-1767'),
    (8, 'silva', '(84) 93619-3588'),
    (9, 'mendes', '(84) 94651-2010'),
    (10, 'francisca', '(84) 98560-3215');

INSERT INTO
    ingredientes (id_ingredientes,nome,quantidade_estoque,validade)
VALUES
    (1, 'Pão de hambúrguer', 100, '2026-12-31'),
    (2, 'Carne bovina', 50, '2026-12-18'),
    (3, 'Queijo cheddar', 30, '2026-11-15'),
    (4, 'Alface', 20, '2026-11-10'),
    (5, 'Tomate', 25, '2026-12-20'),
    (6, 'Cebola', 15, '2026-12-05'),
    (7, 'Bacon', 40, '2026-11-28'),
    (8, 'Leite', 60, '2026-12-01'),
    (9, 'Chocolate', 35, '2026-11-15'),
    (10, 'Ovo', 20, '2026-12-30'),
    (11, 'Batata', 55, '2026-12-04'),
    (12, 'Laranja', 41, '2026-11-05');

INSERT INTO
    itens_do_pedido (id_item,id_pedidos,id_produtos,quantidade,preco_unitario)
VALUES
    (1, 1, 4, 2, 15.20),
    (2, 1, 10, 1, 13.00),
    (3, 2, 7, 1, 15.00),
    (4, 2, 1, 2, 8.00),
    (5, 3, 10, 1, 13.00),
    (6, 3, 9, 1, 10.50),
    (7, 3, 1, 3, 8.00),
    (8, 4, 10, 1, 13.00),
    (9, 5, 7, 2, 15.00),
    (10, 6, 4, 3, 15.20),
    (11, 6, 3, 1, 5.00),
    (12, 7, 1, 1, 8.00),
    (13, 7, 9, 2, 10.50),
    (14, 8, 7, 1, 15.00),
    (15, 8, 10, 2, 13.00),
    (16, 9, 8, 2, 8.49),
    (17, 9, 3, 1, 5.00),
    (18, 10, 1, 2, 8.00);

INSERT INTO pagamentos (id_pagamentos, id_pedidos, forma_pagamento, status_pagamento, data_pagamento, valor_pago)
 VALUES
(1,  1,  'Pix',               'Pago',     '2026-10-26 19:15:00', 43.40),
(2,  2,  'Dinheiro',          'Pendente', '2026-10-27 12:35:00', 31.00),
(3,  3,  'Cartão de crédito', 'Pago',     '2026-10-27 13:10:00', 47.50),
(4,  4,  'Pix',               'Pendente', '2026-10-27 14:05:00', 13.00),
(5,  5,  'Cartão de débito',  'Pago',     '2026-10-26 20:20:00', 30.00),
(6,  6,  'Pix',               'Pago',     '2026-10-28 18:50:00', 50.60),
(7,  7,  'Cartão de débito',  'Pendente', '2026-10-28 19:25:00', 29.00),
(8,  8,  'Dinheiro',          'Pendente', '2026-10-27 11:20:00', 41.00),
(9,  9,  'Pix',               'Pendente', '2026-10-27 12:45:00', 21.98),
(10, 10, 'Cartão de crédito', 'Pago',     '2026-10-27 13:35:00', 16.00);


INSERT INTO pedidos (id_pedidos, id_clientes, status, data_pedidos, tipo_de_entrega)
 VALUES
(1,  1,  'Entregue',         '2026-10-26 19:10:00', 'Entrega'),
(2,  3,  'Em preparo',       '2026-10-27 12:30:00', 'Retirada'),
(3,  5,  'Saiu para entrega','2026-10-27 13:05:00', 'Entrega'),
(4,  2,  'Pendente',         '2026-10-27 14:00:00', 'Retirada'),
(5,  7,  'Entregue',         '2026-10-26 20:15:00', 'Entrega'),
(6,  4,  'Entregue',         '2026-10-28 18:45:00', 'Entrega'),
(7,  6,  'Em preparo',       '2026-10-28 19:20:00', 'Retirada'),
(8,  8,  'Saiu para entrega','2026-10-26 11:15:00', 'Entrega'),
(9,  9,  'Pendente',         '2026-10-26 12:40:00', 'Retirada'),
(10, 10, 'Entregue',         '2026-10-26 13:30:00', 'Entrega');

INSERT INTO produto_ingrediente (id_produtos, id_ingredientes, quantidade_utilizada)
 VALUES
(1,  1,  1),
(1,  2,  1),
(1,  4,  1),
(1,  5,  1),
(2,  11, 10),
(4,  1,  1),
(4,  2,  1),
(4,  3,  3),
(5,  12, 3),
(6,  9,  2),
(6,  10, 3),
(6,  8,  1),
(7,  1,  1),
(7,  2,  2),
(7,  3,  1),
(7,  7,  2),
(7,  4,  1),
(7,  5,  1),
(7,  6,  1),
(8,  1,  1),
(8,  2,  2),
(8,  7,  1),
(8,  5,  2),
(8,  6,  1),
(10, 8,  2),
(10, 9,  4);

INSERT INTO
    produtos (id_produtos, nome, preco, categoria, disponivel)
VALUES
    (1, 'X-salada', 8.00, 'Hambúrguer', 1),
    (2, 'Batata Frita', 7.00, 'Acompanhamento', 0),
    (3, 'Refrigerante Lata', 5.00, 'Bebida', 1),
    (4, 'X-Cheddar', 15.20, 'Hambúrguer', 1),
    (5, 'Suco Laranja', 5.00, 'Bebida', 0),
    (6, 'Brownies', 20.00, 'Sobremesa', 0),
    (7, 'X-King', 15.00, 'Hambúrguer', 1),
    (8, 'Xelita-Burger', 8.49, 'Hambúrguer', 1),
    (9, 'Guaraná Antarctica', 10.50, 'Bebida', 1),
    (10, 'MilkShake', 13.00, 'Sobremesa', 1);

INSERT INTO clientes (id_clientes,nome,telefone)
VALUES
(11,'henrique','(84) 96820-3417'),
(12,'yago','(84) 96514-3018');

--codigos utilizados para atualizar os dados na tabela clientes,
-- para colocar os nomes dos clientes com a primeira letra maiúscula e o restante em minúsculo:
UPDATE clientes SET nome='Augusto' 
WHERE id_clientes=1;

UPDATE clientes SET nome='Jose' 
WHERE id_clientes=2;

UPDATE clientes SET nome='Maria' 
WHERE id_clientes=3;

UPDATE clientes SET nome='Fernando' 
WHERE id_clientes=4;

UPDATE clientes SET nome='Geovana' 
WHERE id_clientes=5;

UPDATE clientes SET nome='Pedro' 
WHERE id_clientes=6;

UPDATE clientes SET nome='Carlos' 
WHERE id_clientes=7;

UPDATE clientes SET nome='Silva' 
WHERE id_clientes=8;

UPDATE clientes SET nome='Mendes' 
WHERE id_clientes=9;

UPDATE clientes SET nome='Francisca' 
WHERE id_clientes=10;
 --codigos utilizados para deletar os dados na tabela clientes, para remover os clientes com id_clientes 11 e 12:
DELETE FROM clientes WHERE id_clientes=12;

DELETE FROM clientes WHERE id_clientes=11;

--codigos utilizados para consultar os dados nas tabelas do banco de dados:

--produtos disponíveis,por categoria e preço:
SELECT categoria, nome, preco
FROM produtos
WHERE disponivel = 1
ORDER BY categoria, preco DESC;

--Pedidos em andamento (fila da cozinha):
SELECT p.id_pedidos, c.nome, p.status, p.data_pedidos
FROM pedidos p
JOIN clientes c USING (id_clientes)
WHERE p.status IN ('Pendente', 'Em preparo', 'Saiu para entrega')
ORDER BY p.data_pedidos;

--Pedidos com nome do cliente e valor total
SELECT p.id_pedidos, c.nome AS cliente, p.status, p.tipo_de_entrega, t.valor_total
FROM pedidos p
JOIN clientes c         ON c.id_clientes = p.id_clientes
JOIN pedidos_com_total t ON t.id_pedidos = p.id_pedidos
ORDER BY t.valor_total ASC;

--Ingredientes de cada produto:
SELECT pr.nome AS produto, GROUP_CONCAT(ing.nome, ', ') AS ingredientes
FROM produtos pr
JOIN produto_ingrediente pi USING (id_produtos)
JOIN ingredientes ing       USING (id_ingredientes)
GROUP BY pr.id_produtos
ORDER BY pr.nome;

--Pedidos entregues com pagamento  confirmado
SELECT p.id_pedidos, c.nome, pg.status_pagamento
FROM pedidos p
JOIN clientes c    USING (id_clientes)
JOIN pagamentos pg USING (id_pedidos)
WHERE p.status = 'Entregue' AND pg.status_pagamento == 'Pago';

--Faturamento total por status de pagamento
SELECT status_pagamento, COUNT(*) AS qtd, ROUND(SUM(valor_pago), 2) AS total
FROM pagamentos
GROUP BY status_pagamento;

--Produtos que nunca foram vendidos:
SELECT nome, categoria
FROM produtos
WHERE id_produtos NOT IN (SELECT id_produtos FROM itens_do_pedido);

--Produtos mais vendidos:
SELECT pr.nome,
       SUM(i.quantidade) AS unidades,
       ROUND(SUM(i.quantidade * i.preco_unitario), 2) AS receita
FROM itens_do_pedido i
JOIN produtos pr ON pr.id_produtos = i.id_produtos
GROUP BY pr.id_produtos
ORDER BY unidades DESC, receita DESC;

--Gasto total por cliente:
SELECT c.nome,
       COUNT(DISTINCT p.id_pedidos) AS pedidos,
       ROUND(COALESCE(SUM(i.quantidade * i.preco_unitario), 0), 2) AS total_gasto
FROM clientes c
JOIN pedidos p               ON p.id_clientes = c.id_clientes
LEFT JOIN itens_do_pedido i  ON i.id_pedidos = p.id_pedidos
GROUP BY c.id_clientes
ORDER BY total_gasto DESC;

--Pedidos com mais de um item distinto:
SELECT id_pedidos, COUNT(*) AS itens_distintos, SUM(quantidade) AS unidades
FROM itens_do_pedido
GROUP BY id_pedidos
HAVING COUNT(*) > 1
ORDER BY unidades DESC;

