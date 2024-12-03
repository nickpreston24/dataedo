This query works on **SQL Server 2016** or newer.

SQL Server 2016 brought exciting and useful feature - **system-versioned temporal tables** that implement change tracking managed by server itself. Server manages 2 separate tables - **system-versioned temporal table** with actual data and **history table** that stores change history.

Query below returns temporal tables paired with their history tables and retention period (how long history is stored).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(t.schema_id) as temporal_table_schema,
     t.name as temporal_table_name,
    schema_name(h.schema_id) as history_table_schema,
     h.name as history_table_name,
    case when t.history_retention_period = -1 
        then 'INFINITE' 
        else cast(t.history_retention_period as varchar) + ' ' + 
            t.history_retention_period_unit_desc + 'S'
    end as retention_period
from sys.tables t
    left outer join sys.tables h
        on t.history_table_id = h.object_id
where t.temporal_type = 2
order by temporal_table_schema, temporal_table_name
```

## Columns

-   **temporal\_table\_schema** - temporal table schema name
-   **temporal\_table\_name** - temporal table name
-   **history\_table\_schema** - history table schema name
-   **history\_table\_name** - history table name
-   **retention\_period** - retention period - how long history is preserved (defined by DBA). Example values: INFINITE, 6 MONTHS, 30 DAYS

## Rows

-   **One row** represents one temporal table
-   **Scope of rows:** only temporal tables defined in a database
-   **Ordered by** temporal table schema and table name

## Sample results

Query returns 3 tables:

![](https://dataedo.com/asset/img/kb/query/sql-server/temporal_tables.png)

And this is how it looks in SSMS:

![](https://dataedo.com/asset/img/kb/query/sql-server/temporal_tables_ssms.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)