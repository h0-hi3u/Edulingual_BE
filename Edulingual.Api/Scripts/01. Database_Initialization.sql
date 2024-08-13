-- Start Create Table

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Role')
BEGIN 
CREATE TABLE [Role] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Name]					VARCHAR(200)						NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'User')
BEGIN
CREATE TABLE [User] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Username]				VARCHAR(200),
	[Email]					VARCHAR(100),
	[Phone]					VARCHAR(10)							NOT NULL,
	[Password]				VARCHAR(100),
	[Fullname]				VARCHAR(100),
	[Description]			TEXT,
	[ImgUrl]				TEXT,
	[Status]				INT,
	[RoleId]				UUID								NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END
IF NOT EXISTS (SELECT 1 FROM INFOMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CourseArea')
BEGIN
CREATE TABLE [CourseArea] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Name]					VARCHAR(200)						NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CourseLanguage')
BEGIN
CREATE TABLE [CourseLanguage] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Name]					VARCHAR(200)						NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CourseCategory')
BEGIN
CREATE TABLE [CourseCategory] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Name]					VARCHAR(200)						NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN	
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Course')
BEGIN
CREATE TABLE [Course] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Title]					VARCHAR(200)						NOT NULL,
	[Description]			TEXT								NOT NULL,
	[Duration]				VARCHAR(200),
	[Fee]					FLOAT								NOT NULL,
	[CourseAreaId]			UUID								NOT NULL,
	[CourseLanguageId]		UUID								NOT NULL,
	[CourseCategoryId]		UUID								NOT NULL,
	[Status]				INT									NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Payment')
BEGIN
CREATE TABLE [Payment] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Fee]					FLOAT								NOT NULL,
	[CourseId]				UUID								NOT NULL,
	[UserId]				UUID								NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserCourse')
BEGIN
CREATE TABLE [UserCourse] (
	[UserId]				UUID								NOT	NULL,
	[CourseId]				UUID								NOT NULL,
	[Status]				INT									NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Exam')
BEGIN
CREATE TABLE [Exam] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Title]					VARCHAR(200)						NOT NULL,
	[CourseId]				UUID								NOT NULL,
	[TotalQuestion]			INT									NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Question')
BEGIN
CREATE TABLE [Question] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Content]				TEXT								NOT NULL,
	[Point]					INT,
	[ExamId]				UUID								NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Answer')
BEGIN
CREATE TABLE [Answer] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Content]				TEXT								NOT NULL,
	[IsCorrect]				BOOLEAN								NOT NULL,
	[QuestionId]			UUID								NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserExam')
BEGIN
CREATE TABLE [UserExam] (
	[Id]					UUID								PRIMARY KEY					DEFAULT NEWID(),
	[Score]					FLOAT								NOT NULL,
	[TotalRight]			INT									NOT NULL,
	[UserId]				UUID								NOT NULL,
	[ExamId]				UUID								NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Feedback')
BEGIN
CREATE TABLE [Feedback] (
	[UserId]				UUID								NOT NULL,
	[CourseId]				UUID								NOT NULL,
	[Content]				VARCHAR(200),
	[Rating]				INT									NOT NULL,
	[CreateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[CreateBy]				UUID								NOT NULL,
	[UpdateAt]				TIMESTAMP(3)						NOT NULL					DEFAULT CURRENT_TIMESTAMP,
	[UpdateBy]				UUID								NOT NULL,
	[IsDeleted]				BOOLEAN								NOT NULL,
)
END
-- End Create Table

-- Start Add Foreign Keys
ALTER TABLE [User]
ADD FOREIGN KEY (RoleId)							REFERENCES [Role](Id)

ALTER TABLE [Course]
ADD FOREIGN KEY (CourseAreaId)						REFERENCES [CourseArea](Id)

ALTER TABLE [Course]
ADD FOREIGN KEY (CourseLanguageId)					REFERENCES [CourseLanguage](Id)

ALTER TABLE [Course]
ADD FOREIGN KEY (CourseCategoryId)					REFERENCES [CourseCategory](Id)

ALTER TABLE [Feedback]
ADD FOREIGN KEY (UserId)							REFERENCES [User](Id)

ALTER TABLE [Feedback]
ADD FOREIGN KEY (CourseId)							REFERENCES [Course](Id)

ALTER TABLE [UserCourse]
ADD FOREIGN KEY (UserId)							REFERENCES [User](Id)

ALTER TABLE [UserCourse]
ADD FOREIGN KEY (CourseId)							REFERENCES [Course](Id)

ALTER TABLE [Payment]
ADD FOREIGN KEY (UserId)							REFERENCES [User](Id)

ALTER TABLE [Payment]
ADD FOREIGN KEY (CourseId)							REFERENCES [Course](Id)

ALTER TABLE [Exam]
ADD FOREIGN KEY (CourseId)							REFERENCES [Course](Id)

ALTER TABLE [UserExam]
ADD FOREIGN KEY	(UserId)							REFERENCES [User](Id)

ALTER TABLE [UserExam]
ADD FOREIGN KEY (ExamId)							REFERENCES [Exam](Id)

ALTER TABLE [Question]
ADD FOREIGN KEY (ExamId)							REFERENCES [Exam](Id)

ALTER TABLE [Answer]
ADD FOREIGN KEY (QuestionId)						REFERENCES [Question](Id)

-- End Add Foreign Key

-- Start Add Composite Key

ALTER TABLE [Feedback]
ADD CONSTRAINT "PK_UserCourseFeedback"				PRIMARY KEY ("UserId", "CourseId")

ALTER TABLE [UserCourse]
ADD CONSTRAINT "PK_UserCourse"						PRIMARY KEY ("UserId", "CourseId")

-- End add Composite key