CREATE DATABASE  IF NOT EXISTS `sportsync_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `sportsync_db`;
-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: sportsync_db
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `coaches`
--

DROP TABLE IF EXISTS `coaches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coaches` (
  `id` int NOT NULL AUTO_INCREMENT,
  `firstname` varchar(200) NOT NULL,
  `lastname` varchar(200) NOT NULL,
  `emailaddress` varchar(200) NOT NULL,
  `phonenumber` varchar(200) NOT NULL,
  `password` varchar(200) NOT NULL,
  `sport` varchar(200) NOT NULL,
  `groupname` varchar(200) DEFAULT NULL,
  `exp` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `groupname_idx` (`groupname`),
  CONSTRAINT `groupname` FOREIGN KEY (`groupname`) REFERENCES `groups` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coaches`
--

LOCK TABLES `coaches` WRITE;
/*!40000 ALTER TABLE `coaches` DISABLE KEYS */;
INSERT INTO `coaches` VALUES (1,'John','Doe','johndoe1@example.com','1234567890','password1','Basketball',NULL,5),(2,'Jane','Smith','janesmith1@example.com','2345678901','password2','Tennis',NULL,8),(3,'Mike','Johnson','mikejohnson1@example.com','3456789012','password3','Swimming',NULL,3),(4,'Emily','Williams','emilywilliams1@example.com','4567890123','password4','Soccer',NULL,10),(5,'Sarah','Brown','sarahbrown1@example.com','5678901234','password5','Volleyball',NULL,7),(6,'Chris','Davis','chrisdavis1@example.com','6789012345','password6','Baseball',NULL,6),(7,'Katie','Miller','katiemiller1@example.com','7890123456','password7','Basketball',NULL,4),(8,'David','Wilson','davidwilson1@example.com','8901234567','password8','Football',NULL,12),(9,'Laura','Moore','lauramoore1@example.com','9012345678','password9','Hockey',NULL,9),(10,'Paul','Taylor','paultaylor1@example.com','1123456789','password10','Gymnastics',NULL,11),(11,'Anna','Anderson','annaanderson1@example.com','2234567890','password11','Running',NULL,2),(12,'James','Thomas','jamesthomas1@example.com','3345678901','password12','Cycling',NULL,15),(13,'Linda','Jackson','lindajackson1@example.com','4456789012','password13','Badminton',NULL,6),(14,'Robert','White','robertwhite1@example.com','5567890123','password14','Rowing',NULL,4),(15,'Sophia','Harris','sophiaharris1@example.com','6678901234','password15','Boxing',NULL,8),(16,'Brian','Martin','brianmartin1@example.com','7789012345','password16','Tennis',NULL,3),(17,'Olivia','Lee','oliviale1@example.com','8890123456','password17','Golf',NULL,7),(18,'Alexander','Clark','alexanderclark1@example.com','9901234567','password18','Basketball',NULL,5),(19,'Ava','Lewis','avalewis1@example.com','1012345678','password19','Skating',NULL,12),(20,'Sophia','Walker','sophiawalker1@example.com','1123456789','password20','Swimming',NULL,2),(21,'Liam','Hall','liamhall1@example.com','2234567890','password21','Football',NULL,13),(22,'Emma','Allen','emmaallen1@example.com','3345678901','password22','Volleyball',NULL,6),(23,'Noah','Young','noahyoung1@example.com','4456789012','password23','Rugby',NULL,9),(24,'Mia','King','miaking1@example.com','5567890123','password24','Basketball',NULL,7),(25,'Lucas','Scott','lucasscott1@example.com','6678901234','password25','Cricket',NULL,10),(26,'Amelia','Green','ameliagreen1@example.com','7789012345','password26','Soccer',NULL,4),(27,'Daniel','Adams','danieladams1@example.com','8890123456','password27','Boxing',NULL,6),(28,'Ella','Baker','ellabaker1@example.com','9901234567','password28','Tennis',NULL,11),(29,'Michael','Gonzalez','michaelgonzalez1@example.com','1012345678','password29','Gymnastics',NULL,8),(30,'Harper','Nelson','harpernelson1@example.com','1123456789','password30','Rowing',NULL,3),(31,'Mason','Carter','masoncarter1@example.com','2234567890','password31','Badminton',NULL,12),(32,'Isabella','Mitchell','isabellamitchell1@example.com','3345678901','password32','Cycling',NULL,7),(33,'Ethan','Perez','ethanperez1@example.com','4456789012','password33','Baseball',NULL,9),(34,'Abigail','Roberts','abigailroberts1@example.com','5567890123','password34','Running',NULL,5),(35,'Jacob','Evans','jacobevans1@example.com','6678901234','password35','Swimming',NULL,2),(36,'Charlotte','Collins','charlottecollins1@example.com','7789012345','password36','Basketball',NULL,10),(37,'Benjamin','Stewart','benjaminstewart1@example.com','8890123456','password37','Football',NULL,14),(38,'Evelyn','Sanchez','evelynsanchez1@example.com','9901234567','password38','Volleyball',NULL,6),(39,'Henry','Morris','henrymorris1@example.com','1012345678','password39','Hockey',NULL,4),(40,'Mila','Rogers','milarogers1@example.com','1123456789','password40','Tennis',NULL,9),(41,'Elijah','Reed','elijahreed1@example.com','2234567890','password41','Soccer',NULL,7),(42,'Scarlett','Cook','scarlettcook1@example.com','3345678901','password42','Rowing',NULL,11),(43,'Sebastian','Morgan','sebastianmorgan1@example.com','4456789012','password43','Gymnastics',NULL,3),(44,'Avery','Bell','averybell1@example.com','5567890123','password44','Cycling',NULL,15),(45,'Jack','Murphy','jackmurphy1@example.com','6678901234','password45','Boxing',NULL,2),(46,'Layla','Bailey','laylabailey1@example.com','7789012345','password46','Running',NULL,10),(47,'William','Rivera','williamrivera1@example.com','8890123456','password47','Basketball',NULL,13),(48,'Zoey','Cooper','zoeycooper1@example.com','9901234567','password48','Football',NULL,6),(49,'Jackson','Richardson','jacksonrichardson1@example.com','1012345678','password49','Golf',NULL,8),(50,'Hannah','Cox','hannahcox1@example.com','1123456789','password50','Skating',NULL,5),(51,'ttt','abaab','rs@','867575558656','2341','football',NULL,9),(52,'raz','perli','rp@','07439393','123','Basketball',NULL,6);
/*!40000 ALTER TABLE `coaches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `groups`
--

DROP TABLE IF EXISTS `groups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `groups` (
  `name` varchar(200) NOT NULL,
  `maxcapacity` int NOT NULL,
  `sport` varchar(200) NOT NULL,
  `coachid` int NOT NULL,
  PRIMARY KEY (`name`),
  KEY `coachid_idx` (`coachid`),
  CONSTRAINT `coachid` FOREIGN KEY (`coachid`) REFERENCES `coaches` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groups`
--

LOCK TABLES `groups` WRITE;
/*!40000 ALTER TABLE `groups` DISABLE KEYS */;
/*!40000 ALTER TABLE `groups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `firstname` varchar(200) NOT NULL,
  `lastname` varchar(200) NOT NULL,
  `emailaddress` varchar(200) NOT NULL,
  `phonenumber` varchar(200) NOT NULL,
  `password` varchar(200) NOT NULL,
  `groupname` varchar(200) DEFAULT NULL,
  `isadmin` varchar(200) NOT NULL,
  `datejoined` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'John','Smith','johnsmith84@gmail.com','(415) 555-0123','BlueSky99',NULL,'false','2024-01-15'),(2,'Emma','Johnson','emma.johnson@yahoo.com','(650) 555-9876','Cookie123',NULL,'false','2024-01-16'),(3,'Michael','Williams','mwilliams_work@outlook.com','(212) 555-4567','Football2024',NULL,'false','2024-01-17'),(4,'Sarah','Brown','sarahb.design@gmail.com','(312) 555-8901','Butterfly24',NULL,'false','2024-01-18'),(5,'David','Jones','davidj.tech@gmail.com','(617) 555-2345','DJ1995',NULL,'false','2024-01-19'),(6,'Lisa','Davis','lisa.davis90@yahoo.com','(408) 555-6789','LuckyDay90',NULL,'false','2024-01-20'),(7,'James','Miller','jamesmiller.pro@gmail.com','(202) 555-1234','Baseball5',NULL,'false','2024-01-21'),(8,'Jennifer','Wilson','jen.wilson@outlook.com','(713) 555-5678','Sparkles22',NULL,'false','2024-01-22'),(9,'Robert','Taylor','rob.taylor@gmail.com','(206) 555-9012','GuitarHero',NULL,'false','2024-01-23'),(10,'Patricia','Anderson','panderson@yahoo.com','(303) 555-3456','Rainbow7',NULL,'false','2024-01-24'),(11,'Thomas','Martinez','tmartinez.work@gmail.com','(305) 555-7890','TomCat55',NULL,'false','2024-02-01'),(12,'Jessica','Garcia','jessicag2024@outlook.com','(619) 555-4321','Dolphin88',NULL,'false','2024-02-02'),(13,'Christopher','Robinson','chris.rob@gmail.com','(415) 555-8765','RedCar123',NULL,'false','2024-02-03'),(14,'Amanda','Clark','amandaclark.biz@yahoo.com','(847) 555-2109','Panda444',NULL,'false','2024-02-04'),(15,'Kevin','Rodriguez','kevinr.dev@gmail.com','(510) 555-6543','Kev2Cool',NULL,'false','2024-02-05'),(16,'Michelle','Lee','michelle.lee@outlook.com','(408) 555-8901','StarLight1',NULL,'false','2024-02-06'),(17,'Daniel','Walker','dan.walker@gmail.com','(213) 555-7654','Walker24',NULL,'false','2024-02-07'),(18,'Elizabeth','Hall','lizhall.pro@yahoo.com','(312) 555-3210','LizzyBear',NULL,'false','2024-02-08'),(19,'Joseph','Allen','joe.allen@gmail.com','(415) 555-9876','Coffee789',NULL,'false','2024-02-09'),(20,'Margaret','Young','margaret.y@outlook.com','(650) 555-5432','Sunset101',NULL,'false','2024-02-10'),(21,'Steven','King','steven.king.work@gmail.com','(917) 555-1098','Crown1234',NULL,'false','2024-02-11'),(22,'Nancy','Wright','nwright.design@yahoo.com','(415) 555-7654','Artist99',NULL,'false','2024-02-12'),(23,'George','Lopez','glopez.tech@gmail.com','(312) 555-3456','Funny123',NULL,'false','2024-02-13'),(24,'Carol','Hill','carolhill88@outlook.com','(718) 555-8907','HillTop88',NULL,'false','2024-02-14'),(25,'Edward','Scott','escott.pro@gmail.com','(415) 555-6543','Scotty123',NULL,'false','2024-02-15'),(26,'Sandra','Green','sandra.green@yahoo.com','(650) 555-2109','GreenTea5',NULL,'false','2024-02-16'),(27,'Brian','Adams','brian.adams@gmail.com','(212) 555-7890','Summer365',NULL,'false','2024-02-17'),(28,'Ashley','Baker','ashleybaker@outlook.com','(415) 555-3456','CupCake22',NULL,'false','2024-02-18'),(29,'Ronald','Gonzalez','rgonzalez.work@gmail.com','(510) 555-8907','Ronnie777',NULL,'false','2024-02-19'),(30,'Kimberly','Nelson','kim.nelson@yahoo.com','(847) 555-6543','Kimmy1995',NULL,'false','2024-02-20'),(31,'Timothy','Carter','timcarter.pro@gmail.com','(213) 555-2109','TimTime24',NULL,'false','2024-02-21'),(32,'Sharon','Mitchell','sharon.m@outlook.com','(415) 555-7890','Beach2024',NULL,'false','2024-02-22'),(33,'Jason','Perez','jasonperez@yahoo.com','(650) 555-3456','Wolf567',NULL,'false','2024-02-23'),(34,'Laura','Roberts','lauraroberts.biz@gmail.com','(312) 555-8907','Dancer91',NULL,'false','2024-02-24'),(35,'Jeffrey','Turner','jeff.turner@outlook.com','(415) 555-6543','Pizza123',NULL,'false','2024-02-25'),(36,'Rebecca','Phillips','rebecca.p@gmail.com','(917) 555-2109','Becky1990',NULL,'false','2024-02-26'),(37,'Gary','Campbell','gary.campbell@yahoo.com','(415) 555-7890','Tiger2024',NULL,'false','2024-02-27'),(38,'Helen','Parker','helen.parker@gmail.com','(650) 555-3456','Garden88',NULL,'false','2024-02-28'),(39,'Stephen','Evans','stephen.evans@outlook.com','(312) 555-8907','Dragon123',NULL,'false','2024-03-01'),(40,'Sandra','Edwards','sedwards.pro@gmail.com','(415) 555-6543','Ocean2024',NULL,'false','2024-03-02'),(41,'Dennis','Collins','dennis.collins@yahoo.com','(510) 555-2109','Music365',NULL,'false','2024-03-03'),(42,'Raz','Perli','razperli100@gmail.com','0527399914','111','','true','18/10/2024');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'sportsync_db'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-10 15:49:12
