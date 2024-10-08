Query that returns tables with number of columns, ordered from the ones that have the most.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select schema_name(tab.schema_id) + '.' + tab.name as [table], 
       count(*) as [columns]
   from sys.tables as tab
        inner join sys.columns as col
            on tab.object_id = col.object_id
group by schema_name(tab.schema_id), 
       tab.name
order by count(*) desc
```

## Columns

-   **table** - name of the table (with schema name)
-   **columns** - number of columns in table

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables in a database
-   **Ordered by** number of columns descending - from tables with the most columns

## Sample results

Here is a statistic from tables in Dataedo repository. Quite a usual database.

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_by_number_of_columns.png)

And this, by comparison is a result from demo **Microsoft Dynamics NAV** database. As you can see it is considerably more complex.

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_by_number_of_columns_nav.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)