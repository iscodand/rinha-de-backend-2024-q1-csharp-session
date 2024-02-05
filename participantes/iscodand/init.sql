-- Create clientes table
CREATE TABLE clientes (
    id INT PRIMARY KEY IDENTITY,
    nome VARCHAR(50) NOT NULL,
    limite INT NOT NULL
);

-- Create transacoes table
CREATE TABLE transacoes (
    id INT PRIMARY KEY IDENTITY,
    cliente_id INT NOT NULL,
    valor INT NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao VARCHAR(10) NOT NULL,
    realizada_em DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT fk_clientes_transacoes_id
        FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

-- Create saldos table
CREATE TABLE saldos (
    id INT PRIMARY KEY IDENTITY,
    cliente_id INT NOT NULL,
    valor INT NOT NULL,
    CONSTRAINT fk_clientes_saldos_id
        FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

-- Insert data using a transaction
BEGIN TRANSACTION;

INSERT INTO clientes (nome, limite)
VALUES
    ('o barato sai caro', 1000 * 100),
    ('zan corp ltda', 800 * 100),
    ('les cruders', 10000 * 100),
    ('padaria joia de cocaina', 100000 * 100),
    ('kid mais', 5000 * 100);

INSERT INTO saldos (cliente_id, valor)
SELECT id, 0 FROM clientes;

COMMIT;
