Query returns basic statistics of numbers of columns in a database.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> [<span>columns</span>], 
    [<span>tables</span>], 
    <span>CONVERT</span>(<span>DECIMAL</span>(<span>10</span>,<span>2</span>),<span>1.0</span>*[<span>columns</span>]/[<span>tables</span>]) <span>as</span> average_column_count
<span>from</span> (
    <span>select</span> <span>count</span>(*) [<span>columns</span>],
           <span>count</span>(<span>distinct</span> schema_name(tab.schema_id) + tab.name) <span>as</span> [<span>tables</span>]
       <span>from</span> sys.tables <span>as</span> tab
            <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
                <span>on</span> tab.object_id = col.object_id
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