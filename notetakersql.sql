CREATE SCHEMA
`notetaker` ;
CREATE TABLE `notetaker`.`notes`
(
  `note_id` INT NOT NULL AUTO_INCREMENT,
  `title` VARCHAR
(45) NOT NULL,
  `note_text` MEDIUMTEXT NULL,
  PRIMARY KEY
(`note_id`),
  UNIQUE INDEX `note_id_UNIQUE`
(`note_id` ASC) VISIBLE);