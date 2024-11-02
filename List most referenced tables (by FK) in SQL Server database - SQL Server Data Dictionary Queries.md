Query below lists tables that are most referenced by other tables with foreign keys. Those are the dictionary tables such as **person**, **product** or **store**. In data warehouses those are dimension tables.

![](https://dataedo.com/asset/img/kb/query/referenced_table.png)

See also:

-   [Tables with most relationships](https://dataedo.com/kb/query/sql-server/list-tables-with-most-relationships) ,
-   [Tables with most foreign keys](https://dataedo.com/kb/query/sql-server/list-tables-with-most-foreign-keys) ,
-   [Summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select schema_name(tab.schema_id) + '.' + tab.name as [table],
   count(fk.name) as [references],
   count(distinct fk.parent_object_id) as referencing_tables
from sys.tables as tab
   left join sys.foreign_keys as fk
       on tab.object_id = fk.referenced_object_id
group by schema_name(tab.schema_id), tab.name
having count(fk.name) &gt; 0
order by 2 desc
```

## Columns

-   **table** - name of the table with schema name
-   **references** - number of foreign keys referencing to this table
-   **referencing\_tables** - number of different tables referencing to this table

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** tables being used in foreign keys as primary key table
-   **Ordered by** number of foregin keys descending

## Sample results

List of most referenced tables in AdventureWorks database. **Production.Product** table is referenced 14 times from 13 different tables.

![](https://dataedo.com/asset/img/kb/query/sql-server/most_referenced_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)