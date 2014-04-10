CREATE SCHEMA `scoreApp` ;

CREATE TABLE `scoreApp`.`Users` (
  `Email` VARCHAR(40) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Email`));


CREATE TABLE `scoreApp`.`Scores` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Reason` VARCHAR(300) NOT NULL,
  `Date` DATETIME NOT NULL,
  `Creator` VARCHAR(40) NOT NULL,
  `Candidate` VARCHAR(40) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `Creator_User_idx` (`Creator` ASC),
  INDEX `Candidate_User_idx` (`Candidate` ASC),
  CONSTRAINT `Creator_User`
    FOREIGN KEY (`Creator`)
    REFERENCES `scoreApp`.`Users` (`Email`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Candidate_User`
    FOREIGN KEY (`Candidate`)
    REFERENCES `scoreApp`.`Users` (`Email`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE TABLE `scoreApp`.`Votes` (
  `ScoreId` INT(11) NOT NULL,
  `User` VARCHAR(40) NOT NULL,
  `Date` DATETIME NOT NULL,
  PRIMARY KEY (`ScoreId`),
  INDEX `User_Vote_idx` (`User` ASC),
  CONSTRAINT `User_Vote`
    FOREIGN KEY (`User`)
    REFERENCES `scoreApp`.`Users` (`Email`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Score_Vote`
    FOREIGN KEY (`ScoreId`)
    REFERENCES `scoreApp`.`Scores` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE TABLE `scoreApp`.`ScoreWitnesses` (
  `ScoreId` INT NOT NULL,
  `Witness` VARCHAR(40) NOT NULL,
  PRIMARY KEY (`ScoreId`),
  INDEX `User_Witness_idx` (`Witness` ASC),
  CONSTRAINT `Score_Witness`
    FOREIGN KEY (`ScoreId`)
    REFERENCES `scoreApp`.`Scores` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `User_Witness`
    FOREIGN KEY (`Witness`)
    REFERENCES `scoreApp`.`Users` (`Email`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);