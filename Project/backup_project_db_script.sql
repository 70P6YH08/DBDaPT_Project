CREATE DATABASE  IF NOT EXISTS `project_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `project_db`;
-- MySQL dump 10.13  Distrib 8.0.45, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: project_db
-- ------------------------------------------------------
-- Server version	8.0.45

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
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `category_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Ботинки'),(2,'Туфли'),(3,'Кроссовки'),(4,'Полуботинки'),(5,'Кеды'),(6,'Тапочки'),(7,'Сапоги');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `maker`
--

DROP TABLE IF EXISTS `maker`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `maker` (
  `maker_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`maker_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maker`
--

LOCK TABLES `maker` WRITE;
/*!40000 ALTER TABLE `maker` DISABLE KEYS */;
INSERT INTO `maker` VALUES (1,'Kari'),(2,'Marco Tozzi'),(3,'Poc'),(4,'Rieker'),(5,'Alessio Nesca'),(6,'CROSBY'),(7,'ARGO'),(8,'FRAU'),(9,'ROMER'),(10,'TOFA'),(11,'Luiza Belly'),(12,'Caprice');
/*!40000 ALTER TABLE `maker` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `order_id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `order_date` date NOT NULL,
  `delivery_date` date NOT NULL,
  `receive_code` int NOT NULL,
  `is_finished` tinyint(1) NOT NULL,
  PRIMARY KEY (`order_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,4,'2025-02-27','2025-04-20',901,1),(2,1,'2022-09-28','2025-04-21',902,1),(3,2,'2025-03-21','2025-04-22',903,1),(4,3,'2025-02-20','2025-04-23',904,1),(5,4,'2025-03-17','2025-04-24',905,1),(6,1,'2025-03-01','2025-04-25',906,1),(7,2,'2025-03-02','2025-04-26',907,1),(8,3,'2025-03-31','2025-04-27',908,0),(9,4,'2025-04-02','2025-04-28',909,0),(10,4,'2025-04-03','2025-04-29',910,0);
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `role_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'admin'),(2,'manager'),(3,'client');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shoe`
--

DROP TABLE IF EXISTS `shoe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shoe` (
  `shoe_id` int NOT NULL AUTO_INCREMENT,
  `article` char(6) NOT NULL,
  `price` int NOT NULL,
  `discount` tinyint NOT NULL DEFAULT '0',
  `quantity` int NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `size` tinyint DEFAULT NULL,
  `color` varchar(50) DEFAULT NULL,
  `vendor_id` int NOT NULL,
  `maker_id` int NOT NULL,
  `category_id` int NOT NULL,
  `is_female` tinyint(1) NOT NULL DEFAULT '0',
  `photo_name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`shoe_id`),
  KEY `vendor_id` (`vendor_id`),
  KEY `maker_id` (`maker_id`),
  KEY `category_id` (`category_id`),
  CONSTRAINT `shoe_ibfk_1` FOREIGN KEY (`vendor_id`) REFERENCES `vendor` (`vendor_id`) ON UPDATE CASCADE,
  CONSTRAINT `shoe_ibfk_2` FOREIGN KEY (`maker_id`) REFERENCES `maker` (`maker_id`) ON UPDATE CASCADE,
  CONSTRAINT `shoe_ibfk_3` FOREIGN KEY (`category_id`) REFERENCES `category` (`category_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shoe`
--

LOCK TABLES `shoe` WRITE;
/*!40000 ALTER TABLE `shoe` DISABLE KEYS */;
INSERT INTO `shoe` VALUES (1,'А112Т4',4990,3,6,'Демисезонные',NULL,NULL,1,1,1,1,'1.jpg'),(2,'F635R4',3244,2,13,'Демисезонные',39,'бежевый',2,2,1,1,'2.jpg'),(3,'H782T5',4499,5,5,'Классика',43,'чёрный',1,1,2,0,'3.jpg'),(4,'G783F5',5900,2,8,'Кожаные с натуральным мехом',NULL,NULL,1,3,1,0,'4.jpg'),(5,'J384T6',3800,0,16,NULL,NULL,NULL,2,4,1,0,'5.jpg'),(6,'D572U8',4100,3,0,NULL,NULL,NULL,2,3,3,0,'6.jpg'),(7,'F572H7',2700,2,14,'Летние',39,'чёрный',1,2,2,1,'7.jpg'),(8,'D329H3',1890,0,4,NULL,37,'бордовый',2,5,4,1,'8.jpg'),(9,'B320R5',4300,2,6,'Демисезонные',41,'коричневый',1,4,2,1,'9.jpg'),(10,'G432E4',2800,0,15,NULL,37,'чёрный',1,1,2,1,'10.jpg'),(11,'S213E3',2156,0,6,NULL,NULL,NULL,2,6,4,0,NULL),(12,'E482R4',1800,2,14,NULL,41,'чёрный',1,1,4,1,NULL),(13,'S634B5',5500,3,0,'Демисезонные',42,'чёрный',2,12,5,0,NULL),(14,'K345R4',2100,2,3,NULL,NULL,NULL,2,6,4,0,NULL),(15,'O754F4',5400,4,18,'Демисезонные',NULL,NULL,2,4,2,1,NULL),(16,'G531F4',6600,0,9,'Зимние',NULL,'чёрный',1,9,1,1,NULL),(17,'J542F5',500,0,0,NULL,NULL,NULL,1,1,6,0,NULL),(18,'B431R5',2700,2,5,'Кожаные',NULL,NULL,2,4,1,0,NULL),(19,'P764G4',6800,10,15,NULL,NULL,NULL,1,7,2,1,NULL),(20,'C436G5',10200,0,0,NULL,40,NULL,1,7,1,1,NULL),(21,'F427R5',11800,15,0,'На молнии, с декоративной пряжкой',NULL,NULL,2,8,1,1,NULL),(22,'N457T5',4600,3,13,'Зимние, мех',NULL,'чёрный',1,6,4,1,NULL),(23,'D364R4',12400,0,5,'Из натуральной замши',NULL,'чёрный',1,11,2,1,NULL),(24,'S326R5',9900,3,15,'Кожаные \"Профиль С.Дали\"',NULL,NULL,2,6,6,0,NULL),(25,'L754R4',1700,2,7,NULL,38,'чёрный',1,1,4,1,NULL),(26,'M542T5',2800,20,3,NULL,NULL,NULL,2,4,3,0,NULL),(27,'D268G5',4399,0,12,'Демисезонные',36,'коричневый',2,4,2,1,NULL),(28,'T324F5',4699,2,5,'Замша',NULL,'синий',1,6,7,1,NULL),(29,'K358H6',599,20,2,NULL,41,'синий',1,4,6,0,NULL),(30,'H535R5',2300,0,7,'Демисезонные',NULL,NULL,2,4,1,1,NULL);
/*!40000 ALTER TABLE `shoe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shoe_order`
--

DROP TABLE IF EXISTS `shoe_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shoe_order` (
  `order_id` int NOT NULL,
  `shoe_id` int NOT NULL,
  `quantity` int NOT NULL,
  PRIMARY KEY (`order_id`,`shoe_id`),
  KEY `shoe_id` (`shoe_id`) /*!80000 INVISIBLE */,
  CONSTRAINT `shoe_order_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `order` (`order_id`) ON UPDATE CASCADE,
  CONSTRAINT `shoe_order_ibfk_2` FOREIGN KEY (`shoe_id`) REFERENCES `shoe` (`shoe_id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shoe_order`
--

LOCK TABLES `shoe_order` WRITE;
/*!40000 ALTER TABLE `shoe_order` DISABLE KEYS */;
INSERT INTO `shoe_order` VALUES (1,1,2),(1,2,2),(2,3,1),(3,5,10),(4,4,1),(4,7,5),(4,8,4),(5,1,2),(5,2,2),(6,3,1),(6,4,1),(7,5,3),(7,6,10),(8,7,5),(8,8,4),(9,6,1),(9,9,5),(9,10,1),(10,11,5);
/*!40000 ALTER TABLE `shoe_order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `role_id` int NOT NULL,
  `first_name` varchar(35) NOT NULL,
  `second_name` varchar(35) NOT NULL,
  `patronymic` varchar(35) NOT NULL,
  `login` varchar(50) NOT NULL,
  `password` char(60) NOT NULL,
  PRIMARY KEY (`user_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,1,'Весения','Никифорова','Николаевна','94d5ous@gmail.com','$2a$11$4jBOGdDUf3K6YymyXaijXufRIjSuBfsR0f8h3v6IGVohnLM9LZVO6'),(2,1,'Руслан','Сазонов','Германович','uth4iz@mail.com','$2a$11$TkYOoEqSOidmxM/9Mi3bS.mZh.SOWYEbzqPnW9XyJFpaaycaXFktW'),(3,1,'Серафим','Одинцов','Артёмович','yzls62@outlook.com','$2a$11$VPvRzWDOYgKu4rlUtlwMsuj2C9CF/WmzmArlBtlWhDERY4nCC3xAi'),(4,2,'Михаил','Степанов','Артёмович','1diph5e@tutanota.com','$2a$11$fjGAE0NWrBISQXh1r7MQT.PKru4w05eYfzAqELnbhGjXXLgACq0Pi'),(5,2,'Петр','Ворсин','Евгеньевич','tjde7c@yahoo.com','$2a$11$l7OtDQeGSioXpAgz/SsGhuD7o5T1VTTFS1TkEKyX5kX0KVxvf6XC2'),(6,2,'Елена','Старикова','Павловна','wpmrc3do@tutanota.com','$2a$11$LTaVL0FHxa9tDxQbhp7NU.qTMnOeudJze8.Yh1VXLbZyc0oFF257G'),(7,3,'Анна','Михайлюк','Вячеславовна','5d4zbu@tutanota.com','$2a$11$0RqU3TSfziVWGQ9Qb3ewkO8REvMw7ARj3HBPngPcGr/FZ60TeDHAq'),(8,3,'Елена','Ситдикова','Анатольевна','ptec8ym@yahoo.com','$2a$11$6kLNVqXqz27BpyCJTsdoGeiB.0h0wyfooYyDcAxtXcZKAErwvSVS6'),(9,3,'Петр','Ворсин','Евгеньевич','1qz4kw@mail.com','$2a$11$aykH/It1r7.WjMI5XbdR..FKMeRqiMeGnWiiJvZSjVNlhofspqyti'),(10,3,'Елена','Старикова','Павловна','4np6se@mail.com','$2a$11$aLOfbqA0Qj/e0s8RdaDyeu7jk3biJhn2bB3i2.dKMxivle0p1RzeW');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendor`
--

DROP TABLE IF EXISTS `vendor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendor` (
  `vendor_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`vendor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendor`
--

LOCK TABLES `vendor` WRITE;
/*!40000 ALTER TABLE `vendor` DISABLE KEYS */;
INSERT INTO `vendor` VALUES (1,'Kari'),(2,'Обувь для вас');
/*!40000 ALTER TABLE `vendor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'project_db'
--

--
-- Dumping routines for database 'project_db'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-22 19:46:47
