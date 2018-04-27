use europadb;


SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `europadb`.`Users`; 
DROP TABLE IF EXISTS `europadb`.`Departments`; 
DROP TABLE IF EXISTS `europadb`.`Roles`; 
SET FOREIGN_KEY_CHECKS = 1; 


DROP PROCEDURE IF EXISTS `drop_all_tables`;

DELIMITER $$
CREATE PROCEDURE `drop_all_tables`()
BEGIN
    DECLARE _done INT DEFAULT FALSE;
    DECLARE _tableName VARCHAR(255);
    DECLARE _cursor CURSOR FOR
        SELECT table_name 
        FROM information_schema.TABLES
        WHERE table_schema = SCHEMA();
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET _done = TRUE;

    SET FOREIGN_KEY_CHECKS = 0;

    OPEN _cursor;

    REPEAT FETCH _cursor INTO _tableName;

    IF NOT _done THEN
        SET @stmt_sql = CONCAT('DROP TABLE ', _tableName);
        PREPARE stmt1 FROM @stmt_sql;
        EXECUTE stmt1;
        DEALLOCATE PREPARE stmt1;
    END IF;

    UNTIL _done END REPEAT;

    CLOSE _cursor;
    SET FOREIGN_KEY_CHECKS = 1;
END$$

DELIMITER ;

call drop_all_tables(); 

DROP PROCEDURE IF EXISTS `drop_all_tables`;


/****** Object:  Table Users    Script Date: 17/04/2018  ******/
CREATE TABLE Users(
	UserId int AUTO_INCREMENT NOT NULL,
	UserName varchar(20) NOT NULL,
	Forename varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	StartDate datetime NULL,
	RoleId int NOT NULL,
	DeptId varchar(50) NOT NULL,
	Leaver varchar(30) NOT NULL,
	LeavingDate datetime NULL,
CONSTRAINT PK_UserId PRIMARY KEY CLUSTERED 
(
	UserId ASC
));



/****** Object:  Table Departments    Script Date: 08/07/2017 ******/

CREATE TABLE Departments(
	DeptId int AUTO_INCREMENT NOT NULL,
	DeptName varchar(50) NOT NULL,
 CONSTRAINT PK_Departament PRIMARY KEY CLUSTERED 
(
	DeptId ASC
));

/****** Object:  Table Roles Script Date: 08/07/2017  ******/

CREATE TABLE Roles(
	RoleID int AUTO_INCREMENT NOT NULL,
	Role varchar(50) NOT NULL,
 CONSTRAINT PK_Role PRIMARY KEY CLUSTERED 
(
	RoleID ASC
));



/*
# ---------------------------------------------------------------------- #
# Foreign keys                                                           #
# ---------------------------------------------------------------------- #
*/	
ALTER TABLE Users ADD CONSTRAINT FK_UserVsRole 
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);
	
ALTER TABLE Users ADD CONSTRAINT FK_UserVsDepartment 
    FOREIGN KEY (DeptId) REFERENCES Departments(DeptId);	


/*
# ---------------------------------------------------------------------- #
# Inserts                                                 #
# ---------------------------------------------------------------------- #
*/	


INSERT INTO `Departments` (`DeptName`) VALUES('Group');
INSERT INTO `Departments` (`DeptName`) VALUES('Software Development');

INSERT INTO `Roles` (`Role`) VALUES('IT Director');
INSERT INTO `Roles` (`Role`) VALUES('Development Manager');
INSERT INTO `Roles` (`Role`) VALUES('Software Developer');


	INSERT INTO `Users` (`UserName`,`Forename`,`Surname`,`StartDate`,`RoleId`,`DeptId`,`Leaver`) 
	VALUES('jbloggs','Joe','Bloggs','2014-03-01',3,2, 0);

	INSERT INTO `Users` (`UserName`,`Forename`,`Surname`,`StartDate`,`RoleId`,`DeptId`,`Leaver`) 
	VALUES('jsmith','John','Smith','2015-11-13',2,2, 0);

	INSERT INTO `Users` (`UserName`,`Forename`,`Surname`,`StartDate`,`RoleId`,`DeptId`,`Leaver`,`LeavingDate`) 
	VALUES('dvogle','Dan','Vogle','2009-05-09',3,2, 1, '2011-09-23');

	INSERT INTO `Users` (`UserName`,`Forename`,`Surname`,`StartDate`,`RoleId`,`DeptId`,`Leaver`) 
	VALUES('dvogle','Dan','Vogle','2009-05-09',1,1, 0);





