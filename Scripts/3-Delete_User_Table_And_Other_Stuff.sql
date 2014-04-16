ALTER TABLE `scoreApp`.`Scores` 
DROP FOREIGN KEY `Candidate_User`,
DROP FOREIGN KEY `Creator_User`;
ALTER TABLE `scoreApp`.`Scores` 
ADD COLUMN `TimeUp` BIT NOT NULL AFTER `Candidate`,
DROP INDEX `Candidate_User_idx` ,
DROP INDEX `Creator_User_idx` ;

ALTER TABLE `scoreApp`.`Votes` 
DROP FOREIGN KEY `User_Vote`;
ALTER TABLE `scoreApp`.`Votes` 
DROP PRIMARY KEY,
ADD PRIMARY KEY (`ScoreId`, `User`),
DROP INDEX `User_Vote_idx` ;


ALTER TABLE `scoreApp`.`ScoreWitnesses` 
DROP FOREIGN KEY `User_Witness`;
ALTER TABLE `scoreApp`.`ScoreWitnesses` 
DROP PRIMARY KEY,
ADD PRIMARY KEY (`ScoreId`, `Witness`),
DROP INDEX `User_Witness_idx` ;


DROP TABLE `scoreApp`.`Users`;