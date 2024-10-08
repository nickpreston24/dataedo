Queries below return server version, edition and system information.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query 1 - Raw

### Query

```
select @@version as version
```

### Columns

-   **version** - string containing SQL Server version and system information

### Rows

Query returns just **one row**

### Sample results

SQL Server 2017:

![](https://dataedo.com/asset/img/kb/query/sql-server/server_version_2017.png)

SQL Server 2012:

![](https://dataedo.com/asset/img/kb/query/sql-server/server_version_2012.png)

SQL Server 2008:

![](https://dataedo.com/asset/img/kb/query/sql-server/server_version_2008.png)

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query 2 - Formatted

### Query

```
SELECT 
    case when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '8.0%' then 'SQL Server 2000'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '9.0%' then 'SQL Server 2005'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '10.0%' then 'SQL Server 2008'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '10.5%' then 'SQL Server 2008 R2'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '11.0%' then 'SQL Server 2012'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '12.0%' then 'SQL Server 2014'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '13.0%' then 'SQL Server 2016'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            like '14.0%' then 'SQL Server 2017'
        when CONVERT(sysname, SERVERPROPERTY('ProductVersion')) 
            &gt;  '14.0.9' then 'newer than SQL Server 2017'
        else 'unknown'
    end as [version],
    SERVERPROPERTY('Edition') as [edition]
```

### Columns

-   **version** - SQL Server version, e.g. SQL Server 2017
-   **edition** - SQL Server edition, e.g. Standard Edition

### Rows

Query returns just **one row**

### Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/server_version_edition_2012.png)

## SSMS

You can check find information in SSMS by the server address in Object Explorer.

![](https://dataedo.com/asset/img/kb/query/sql-server/server_version_ssms.png)

This is major.minor version numbers. You can translate them to version names with this table:

| Version no | Version name |
| --- | --- |
| 8.0 | 2000 |
| 9.0 | 2005 |
| 10.0 | 2008 |
| 10.5 | 2008 R2 |
| 11.0 | 2012 |
| 12.0 | 2014 |
| 13.0 | 2016 |
| 14.0 | 2017 |

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)