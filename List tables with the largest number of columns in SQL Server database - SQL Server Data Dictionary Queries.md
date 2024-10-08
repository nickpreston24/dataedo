Query that returns tables with number of columns, ordered from the ones that have the most.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>], 
       <span>count</span>(*) <span>as</span> [<span>columns</span>]
   <span>from</span> sys.tables <span>as</span> tab
        <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
            <span>on</span> tab.object_id = col.object_id
<span>group</span> <span>by</span> schema_name(tab.schema_id), 
       tab.name
<span>order</span> <span>by</span> <span>count</span>(*) <span>desc</span>
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