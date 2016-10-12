# TestCasesInventory
## Getting Started

### Requirements

  * Mac OS X, Windows, or Linux
  * [Node.js](https://nodejs.org/) v5.0 or newer
  * MVC 5, .Net Frame > 4.5
  * `npm` v3.3 or newer (new to [npm](https://docs.npmjs.com/)?)
  
### Directory Layout

Before you start, take a moment to see how the project structure looks like:

```
.
|-- Root
    |-- .gitignore									# Git ignore file
    |-- README.md 									# Readme file
    |-- TestCasesInventory.sln 						# Solution file
    |-- Documentations 								# Folder contains documentations related to the project
    |-- Libaries 									# External libraries
    |-- packages    								# External libraries provided by nuget
    |-- TestCasesInventory  						# Web root project
    |   |-- App_Data 								# Contains data related files
    |   |-- App_Start								# Contains files called when the app starts
    |   |-- Areas									# Areas (Admin and Test)
    |   |   |-- Admin								# Admin area
    |   |       |-- Controllers						# Controller for Admin Area
    |   |       |-- Models 							# Models for Admin Area
    |   |       |-- Views 							# Views for Admin Area
    |   |           |-- Shared    					# Shared Views for Admin Area
    |   |-- Bindings    							# Custom Bindings
    |   |-- ClientSide 								# ClientSide Files goes here
    |   |   |-- Scripts 							# Scripts used in the app
    |   |   |   |-- main.js 						# Startup script
    |   |   |   |-- App 							# App business scripts
    |   |   |   |-- Lib    							# Javascript libraries used in the project
    |   |   |-- Templates 							# Html templates used in the app
    |   |-- Content   								# Css and images
    |   |-- Controllers 							# Controllers
    |   |-- fonts 									# fonts
    |   |-- logs 									# logs
    |   |-- Models 									# Models
    |   |-- Views   								# Views
    |-- TestCasesInventory.Common 					# Common project used for TestCasesInventory
    |-- TestCasesInventory.Config    				# Configuration information for TestCasesInventory
    |-- TestCasesInventory.Data    					# Data project
    |   |-- DataModels 								# Data models such as user
    |   |-- Migrations 								# Entity framework migration
    |   |-- Repositories         					# Repositories classes
    |-- TestCasesInventory.Data.Common    			# Common project used for Data project
    |-- TestCasesInventory.Data.Config 				# Configuration information for Data project
    |-- TestCasesInventory.Presenter  				# Presenter project. This project will contain core businesses of the app
    |   |-- Business 								# Business classes
    |   |-- Mappings								# Mapping from data models to view models
    |   |-- Models 									# View models
    |   |-- Validations   							# View models validation
    |-- TestCasesInventory.Presenter.Common  		# Common project for Presenter  
    |-- TestCasesInventory.Presenter.Config 		# Configuration information Presenter project
    |-- TestCasesInventory.Presenter.Synchroniser 	# Synchronizer project to update data for read-only properties using Observer Pattern
    |-- TestCasesInventory.Web.Common 				# The shared utilities amongst multiple layers
        |-- Base 									# Base classes
        |-- Utils 									# Utilities

```

**Note**: The project is preferred to host on IIS 8 or higher and using SQL 2012.

### Quick Start

#### 1. Get the latest version


### How to Build, Test, Deploy


### How to Update


