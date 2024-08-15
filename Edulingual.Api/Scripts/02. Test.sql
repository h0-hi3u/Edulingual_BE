--- Start Create Table

--DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Role')
        CREATE TABLE [Role] (
            [Id]                    UUID                    PRIMARY KEY                   DEFAULT GEN_RANDOM_UUID(),
            [Name]                  VARCHAR(200)            NOT NULL,
            [CreateAt]              TIMESTAMP(3)            NOT NULL                      DEFAULT CURRENT_TIMESTAMP,
            [CreateBy]              UUID                    NOT NULL,
            [UpdateAt]              TIMESTAMP(3)            NOT NULL                      DEFAULT CURRENT_TIMESTAMP,
            [UpdateBy]              UUID                    NOT NULL,
            [IsDeleted]             BOOLEAN                 NOT NULL
        );
    END 
--END $$;


--DO $$
--BEGIN
--	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'User') THEN
--		CREATE TABLE [User] (
--			[Id]					UUID					PRIMARY KEY						DEFAULT GEN_RANDOM_UUID(),
--			[Username]				VARCHAR(200),
--			[Email]					VARCHAR(100),
--			[Phone]					VARCHAR(10)				NOT NULL,
--			[Password]				VARCHAR(100),
--			[Fullname]				VARCHAR(100),
--			[Description]			TEXT,
--			[Status]				INT,
--			[RoleId]				UUID					NOT NULL,
--			[CreateAt]              TIMESTAMP(3)            NOT NULL                      DEFAULT CURRENT_TIMESTAMP,
--            [CreateBy]              UUID                    NOT NULL,
--            [UpdateAt]              TIMESTAMP(3)            NOT NULL                      DEFAULT CURRENT_TIMESTAMP,
--            [UpdateBy]              UUID                    NOT NULL,
--            [IsDeleted]             BOOLEAN                 NOT NULL,
--		);
--	END IF;
--END $$;