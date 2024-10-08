Query below returns the average number of columns per table in a database.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select [columns], 
    [tables], 
    CONVERT(DECIMAL(10,2),1.0*[columns]/[tables]) as average_column_count
from (
    select count(*) [columns],
           count(distinct schema_name(tab.schema_id) + tab.name) as [tables]
       from sys.tables as tab
            inner join sys.columns as col
                on tab.object_id = col.object_id
) q
```

## Columns

-   **columns** - total number of columns in a database
-   **tables** - number of tables in a database
-   **average\_column\_count** - average number of columns in a table in a database

## Rows

-   Query returns just **one row**

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/columns_number.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)