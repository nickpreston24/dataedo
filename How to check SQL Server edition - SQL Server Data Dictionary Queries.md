Queries below return product and server edition, i.e. Express, Developer, Standard, Enterprise, etc.

## Installed product

This query returns installed product edition of the instance of SQL Server. Use it to determine the features and the limits.

### Query

```
<span>select</span> SERVERPROPERTY(<span>'Edition'</span>) <span>as</span> [<span>edition</span>]
```

### Columns

-   **edition** - SQL Server edition:
    -   Enterprise Edition
    -   Enterprise Edition: Core-based Licensing
    -   Enterprise Evaluation Edition
    -   Business Intelligence Edition
    -   Developer Edition
    -   Express Edition
    -   Express Edition with Advanced Services
    -   Standard Edition
    -   Web Edition
    -   SQL Azure

### Rows

Query returns just **one row**

### Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/server_edition_express.png)

## Edition

Returns database engine edition.

### Query

```
<span>select</span>
    <span>case</span> <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>1</span> <span>then</span> <span>'Personal/Desktop'</span>
        <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>2</span> <span>then</span> <span>'Standard'</span>
        <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>3</span> <span>then</span> <span>'Enterprise'</span>
        <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>4</span> <span>then</span> <span>'Express'</span>
        <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>5</span> <span>then</span> <span>'SQL Database'</span>
        <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>6</span> <span>then</span> <span>'SQL Data Warehouse'</span>
        <span>when</span> SERVERPROPERTY(<span>'EngineEdition'</span>) = <span>8</span> <span>then</span> <span>'Managed Instance'</span>
    <span>end</span> <span>as</span> [<span>edition</span>]
```

### Columns

-   **edition** - SQL Server edition:
    -   Personal/Desktop
    -   Standard - Standard, Web, and Business Intelligence
    -   Enterprise - Evaluation, Developer, and both Enterprise editions
    -   Express - Express, Express with Tools and Express with Advanced Services
    -   SQL Database - Azure SQL Database
    -   SQL Data Warehouse - Azure SQL Datawarehouse
    -   Managed Instance

### Rows

Query returns just **one row**

### Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/server_edition2_express.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)