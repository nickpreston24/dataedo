If you want to get an overview on how many rows tables in your database hold one way is to count them by row intervals. This query returns number of tables by the number of their rows grouped into predefined intervals.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select
    row_count,
    count(*) tables
from 
    (select 
        [table], 
            case when rows &gt; 1000000000 then '1b rows and more'
                when rows &gt; 1000000 then '1m - 1b rows'
                when rows &gt; 1000 then '1k - 1m rows'
                when rows &gt; 100 then '100 - 1k rows'
                when rows &gt; 10 then '10 - 100 rows'
                else  '0 - 10 rows' end as row_count,
        rows as sort
    from
        (
        select schema_name(tab.schema_id) + '.' + tab.name as [table], 
               sum(part.rows) as [rows]
           from sys.tables tab
                inner join sys.partitions part
                    on tab.object_id = part.object_id
        where part.index_id IN (1, 0) -- 0 - table without PK, 1 table with PK
        group by schema_name(tab.schema_id) + '.' + tab.name
        ) q
    ) a
group by row_count
order by max(sort)
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