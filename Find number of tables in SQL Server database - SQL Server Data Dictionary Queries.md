Query below returns total number of tables in current database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> <span>count</span>(*) <span>as</span> [<span>tables</span>]
   <span>from</span> sys.tables 
```

## Columns

-   **tables** - number of tables in a database

## Rows

Query returns just **one row**.

## Sample results

Those results show that there are 73 tables in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/number_of_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)