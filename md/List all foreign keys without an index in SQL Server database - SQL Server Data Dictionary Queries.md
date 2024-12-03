## Why

Creating an index on foreign key columns is generally considered a good practice. In most cases, it will enhance queries performance, as those columns will be widely used for joining related tables. Sometimes developers forget to create such indexes, or simply don't know that they can improve performance. Missing indices are usually identified only when users report long execution or loading times. It is much better to check databases every now and then and create indexeses in timely manner.

Under some circumstances however, such as low selectivity (many repeated values in a column) or a database where inserts/updates/deletes are much more frequent than selects, you should carefully consider adding an index as maintaining it may be more costly than performance gain when querying. As always with any databse solution **it depends**.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

### SQL Server 2017 and above

This query will not only list foreign keys that lack indexes, but also generate DDL to create them:

```
SELECT 
    fk.name AS foreign_key_name,
    t_parent.name AS table_name,
    'CREATE NONCLUSTERED INDEX IX_' + t_parent.name + '_' + STRING_AGG(c_parent.name, '_') + ' ON ' + t_parent.name
    + ' (' + STRING_AGG(c_parent.name, ',') + ');' AS idx_ddl
FROM 
    sys.foreign_keys fk 
INNER JOIN sys.foreign_key_columns fkc ON 
    fkc.constraint_object_id = fk.object_id
INNER JOIN sys.tables t_parent ON 
    t_parent.object_id = fk.parent_object_id
INNER JOIN sys.columns c_parent ON 
    fkc.parent_column_id = c_parent.column_id  
    AND 
    c_parent.object_id = t_parent.object_id 
LEFT JOIN sys.index_columns idx_parent ON
    t_parent.object_id = idx_parent.object_id
    AND
    c_parent.column_id = idx_parent.column_id
WHERE
    idx_parent.index_column_id IS NULL
GROUP BY
    fk.name,
    t_parent.name
```

**Sample results**

![sql_server_2017_results](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-all-foreign-keys-without-an-index-in-sql-server-database/0b76effa00de319ee51f63da549cfcf9.png#center "sql_server_2017_results")

### SQL Server before 2017

```
SELECT 
    fk.name AS foreign_key_name,
    t_parent.name AS table_name,
    c_parent.name AS column_name
FROM 
    sys.foreign_keys fk 
INNER JOIN sys.foreign_key_columns fkc ON 
    fkc.constraint_object_id = fk.object_id
INNER JOIN sys.tables t_parent ON 
    t_parent.object_id = fk.parent_object_id
INNER JOIN sys.columns c_parent ON 
    fkc.parent_column_id = c_parent.column_id  
    AND 
    c_parent.object_id = t_parent.object_id 
LEFT JOIN sys.index_columns idx_parent ON
    t_parent.object_id = idx_parent.object_id
    AND
    c_parent.column_id = idx_parent.column_id
WHERE
    idx_parent.index_column_id IS NULL
```

**Sample results**

![sql_server_2017_below_results](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-all-foreign-keys-without-an-index-in-sql-server-database/cd77d4876dc3e24e2ec055084c3d147f.png#center "sql_server_2017_below_results")