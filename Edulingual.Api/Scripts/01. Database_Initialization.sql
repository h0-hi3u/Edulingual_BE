--- Start Create Table
DO $$
BEGIN
    ---Create Table Role
    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Role') THEN
        CREATE TABLE "Role" (
        "Id"						UUID							PRIMARY KEY         DEFAULT GEN_RANDOM_UUID(),
        "Name"						VARCHAR(200)					NOT NULL,
        "CreateAt"					TIMESTAMP(3)					NOT NULL            DEFAULT CURRENT_TIMESTAMP,
        "CreateBy"					UUID							NOT NULL,
        "UpdateAt"					TIMESTAMP(3)					NOT NULL            DEFAULT CURRENT_TIMESTAMP,
        "UpdateBy"					UUID							NOT NULL,
        "IsDeleted"					BOOLEAN							NOT NULL
    );
    END IF;
    ---Create Table User
	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'User') THEN
		CREATE TABLE "User" (
		"Id"						UUID							PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Username"					VARCHAR(200),
		"Email"						VARCHAR(100),
		"Phone"						VARCHAR(10)						NOT NULL,
		"Password"					VARCHAR(100),
		"Fullname"					VARCHAR(100),
		"Description"				TEXT,
		"Status"					INT,
		"RoleId"					UUID							NOT NULL,
		"CreateAt"					TIMESTAMP(3)					NOT NULL            DEFAULT CURRENT_TIMESTAMP,
        "CreateBy"					UUID							NOT NULL,
        "UpdateAt"					TIMESTAMP(3)					NOT NULL            DEFAULT CURRENT_TIMESTAMP,
        "UpdateBy"					UUID							NOT NULL,
        "IsDeleted"					BOOLEAN							NOT NULL
	);
	END IF;

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CourseArea') THEN
    CREATE TABLE "CourseArea" (
	    "Id"					UUID								PRIMARY KEY         DEFAULT GEN_RANDOM_UUID(),
	    "Name"					VARCHAR(200)						NOT NULL,
	    "CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
	    "CreateBy"				UUID								NOT NULL,
	    "UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
	    "UpdateBy"				UUID								NOT NULL,
	    "IsDeleted"				BOOLEAN								NOT NULL
    );
    END IF;

    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CourseLanguage') THEN
	CREATE TABLE "CourseLanguage" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Name"					VARCHAR(200)						NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CourseCategory') THEN
	CREATE TABLE "CourseCategory" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Name"					VARCHAR(200)						NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN	
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Course') THEN
	CREATE TABLE "Course" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Title"					VARCHAR(200)						NOT NULL,
		"Description"			TEXT								NOT NULL,
		"Duration"				VARCHAR(200),
		"Fee"					FLOAT								NOT NULL,
		"CourseAreaId"			UUID								NOT NULL,
		"CourseLanguageId"		UUID								NOT NULL,
		"CourseCategoryId"		UUID								NOT NULL,
		"OwnerId"				UUID								NOT NULL,
		"Status"				INT									NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Payment') THEN
	CREATE TABLE "Payment" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Fee"					FLOAT								NOT NULL,
		"CourseId"				UUID								NOT NULL,
		"UserId"				UUID								NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserCourse') THEN
	CREATE TABLE "UserCourse" (
		"UserId"				UUID								NOT	NULL,
		"CourseId"				UUID								NOT NULL,
		"Status"				INT									NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Exam') THEN
	CREATE TABLE "Exam" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Title"					VARCHAR(200)						NOT NULL,
		"CourseId"				UUID								NOT NULL,
		"TotalQuestion"			INT									NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Question') THEN
	CREATE TABLE "Question" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Content"				TEXT								NOT NULL,
		"Point"					INT,
		"ExamId"				UUID								NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Answer') THEN
	CREATE TABLE "Answer" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Content"				TEXT								NOT NULL,
		"IsCorrect"				BOOLEAN								NOT NULL,
		"QuestionId"			UUID								NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserExam') THEN
	CREATE TABLE "UserExam" (
		"Id"					UUID								PRIMARY KEY			DEFAULT GEN_RANDOM_UUID(),
		"Score"					FLOAT								NOT NULL,
		"TotalRight"			INT									NOT NULL,
		"UserId"				UUID								NOT NULL,
		"ExamId"				UUID								NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;

	IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Feedback') THEN
	CREATE TABLE "Feedback" (
		"UserId"				UUID								NOT NULL,
		"CourseId"				UUID								NOT NULL,
		"Content"				VARCHAR(200),
		"Rating"				INT									NOT NULL,
		"CreateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"CreateBy"				UUID								NOT NULL,
		"UpdateAt"				TIMESTAMP(3)						NOT NULL			DEFAULT CURRENT_TIMESTAMP,
		"UpdateBy"				UUID								NOT NULL,
		"IsDeleted"				BOOLEAN								NOT NULL
	);
	END IF;
--- End Create Table

--- Start Add Foreign Key
    ALTER TABLE "User"
    ADD CONSTRAINT FK_User_Role
    FOREIGN KEY ("RoleId") REFERENCES "Role"("Id")
    ON DELETE SET NULL;

	ALTER TABLE "Course"
	ADD CONSTRAINT FK_Course_Area
	FOREIGN KEY ("CourseAreaId") REFERENCES "CourseArea"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Course"
	ADD CONSTRAINT FK_Course_Language
	FOREIGN KEY ("CourseLanguageId") REFERENCES "CourseLanguage"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Course"
	ADD CONSTRAINT FK_Course_Category
	FOREIGN KEY ("CourseCategoryId") REFERENCES "CourseCategory"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Course"
	ADD CONSTRAINT FK_Course_Owner
	FOREIGN KEY ("OwnerId") REFERENCES "User"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Feedback"
	ADD CONSTRAINT FK_Feedback_User
	FOREIGN KEY ("UserId") REFERENCES "User"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Feedback"
	ADD CONSTRAINT FK_Feedback_Course
	FOREIGN KEY ("CourseId") REFERENCES "Course"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "UserCourse"
	ADD CONSTRAINT FK_UserCourse_User
	FOREIGN KEY ("UserId") REFERENCES "User"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "UserCourse"
	ADD CONSTRAINT FK_UserCourse_Course
	FOREIGN KEY ("CourseId") REFERENCES "Course"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Payment"
	ADD CONSTRAINT FK_Payment_User
	FOREIGN KEY ("UserId") REFERENCES "User"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Payment"
	ADD CONSTRAINT FK_Payment_Course
	FOREIGN KEY ("CourseId") REFERENCES "Course"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Exam"
	ADD CONSTRAINT FK_Exam_Course
	FOREIGN KEY ("CourseId") REFERENCES "Course"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "UserExam"
	ADD CONSTRAINT FK_UserExam_User
	FOREIGN KEY ("UserId") REFERENCES "User"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "UserExam"
	ADD CONSTRAINT FK_UserExam_Exam
	FOREIGN KEY ("ExamId") REFERENCES "Exam"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Question"
	ADD CONSTRAINT FK_Question_Exam
	FOREIGN KEY ("ExamId") REFERENCES "Exam"("Id")
	ON DELETE SET NULL;

	ALTER TABLE "Answer"
	ADD CONSTRAINT FK_Answer_Question
	FOREIGN KEY ("QuestionId") REFERENCES "Question"("Id")
	ON DELETE SET NULL;
--- End Add Foreign Key

--- Start Composite Key
	ALTER TABLE "Feedback"
	ADD CONSTRAINT PK_Feedback
	PRIMARY KEY ("UserId", "CourseId");

	ALTER TABLE "UserCourse"
	ADD CONSTRAINT FK_UserCourse
	PRIMARY KEY ("UserId", "CourseId");
--- End Composite Key
END $$;