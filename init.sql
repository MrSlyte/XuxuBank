CREATE TABLE IF NOT EXISTS public.Clients
(
    Id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    Name character varying(150) COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT Clients_pkey PRIMARY KEY (Id)
);



CREATE TABLE IF NOT EXISTS public.ClientTransactions
(
    Id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    ClientId integer NOT NULL,
    Type character varying(2) COLLATE pg_catalog.default NOT NULL,
    Value bigint NOT NULL,
    Description character varying(10) COLLATE pg_catalog.default NOT NULL,
    Date date NOT NULL,
    CONSTRAINT ClientTransactions_pkey PRIMARY KEY (Id),
    CONSTRAINT fk_clients_clientTransactions FOREIGN KEY (ClientId)
        REFERENCES public.Clients (Id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);
