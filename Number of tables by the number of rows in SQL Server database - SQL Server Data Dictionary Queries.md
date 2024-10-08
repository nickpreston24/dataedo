If you want to get an overview on how many rows tables in your database hold one way is to count them by row intervals. This query returns number of tables by the number of their rows grouped into predefined intervals.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span>
    <span>row_count</span>,
    <span>count</span>(*) <span>tables</span>
<span>from</span> 
    (<span>select</span> 
        [<span>table</span>], 
            <span>case</span> <span>when</span> <span>rows</span> &gt; <span>1000000000</span> <span>then</span> <span>'1b rows and more'</span>
                <span>when</span> <span>rows</span> &gt; <span>1000000</span> <span>then</span> <span>'1m - 1b rows'</span>
                <span>when</span> <span>rows</span> &gt; <span>1000</span> <span>then</span> <span>'1k - 1m rows'</span>
                <span>when</span> <span>rows</span> &gt; <span>100</span> <span>then</span> <span>'100 - 1k rows'</span>
                <span>when</span> <span>rows</span> &gt; <span>10</span> <span>then</span> <span>'10 - 100 rows'</span>
                <span>else</span>  <span>'0 - 10 rows'</span> <span>end</span> <span>as</span> <span>row_count</span>,
        <span>rows</span> <span>as</span> <span>sort</span>
    <span>from</span>
        (
        <span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>], 
               <span>sum</span>(part.rows) <span>as</span> [<span>rows</span>]
           <span>from</span> sys.tables tab
                <span>inner</span> <span>join</span> sys.partitions part
                    <span>on</span> tab.object_id = part.object_id
        <span>where</span> part.index_id <span>IN</span> (<span>1</span>, <span>0</span>) <span>-- 0 - table without PK, 1 table with PK</span>
        <span>group</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name
        ) q
    ) a
<span>group</span> <span>by</span> <span>row_count</span>
<span>order</span> <span>by</span> <span>max</span>(<span>sort</span>)
```

## Columns

-   **row\_count** - predefined row count intervals:
    -   0 - 10 rows
    -   10 - 100 rows
    -   100 - 1k rows
    -   1k - 1m rows
    -   1m - 1b rows
    -   1b rows and more
-   **tables** - number of tables that row count falls in that interval

## Rows

-   **One row** represents one interval
-   **Scope of rows:** all row count intervals that appear in the database
-   **Ordered by** from smallest tables to the largest

## Sample results

Here is a number of tables by row count in AdventureWorks database aggregated into predefined intervals.

![](https://dataedo.com/asset/img/kb/query/sql-server/row_count_intervals.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)