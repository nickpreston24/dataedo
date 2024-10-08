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
<span>SELECT</span> 
    fk.name <span>AS</span> foreign_key_name,
    t_parent.name <span>AS</span> table_name,
    <span>'CREATE NONCLUSTERED INDEX IX_'</span> + t_parent.name + <span>'_'</span> + STRING_AGG(c_parent.name, <span>'_'</span>) + <span>' ON '</span> + t_parent.name
    + <span>' ('</span> + STRING_AGG(c_parent.name, <span>','</span>) + <span>');'</span> <span>AS</span> idx_ddl
<span>FROM</span> 
    sys.foreign_keys fk 
<span>INNER</span> <span>JOIN</span> sys.foreign_key_columns fkc <span>ON</span> 
    fkc.constraint_object_id = fk.object_id
<span>INNER</span> <span>JOIN</span> sys.tables t_parent <span>ON</span> 
    t_parent.object_id = fk.parent_object_id
<span>INNER</span> <span>JOIN</span> sys.columns c_parent <span>ON</span> 
    fkc.parent_column_id = c_parent.column_id  
    <span>AND</span> 
    c_parent.object_id = t_parent.object_id 
<span>LEFT</span> <span>JOIN</span> sys.index_columns idx_parent <span>ON</span>
    t_parent.object_id = idx_parent.object_id
    <span>AND</span>
    c_parent.column_id = idx_parent.column_id
<span>WHERE</span>
    idx_parent.index_column_id <span>IS</span> <span>NULL</span>
<span>GROUP</span> <span>BY</span>
    fk.name,
    t_parent.name
```

**Sample results**

![sql_server_2017_results](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-all-foreign-keys-without-an-index-in-sql-server-database/0b76effa00de319ee51f63da549cfcf9.png#center "sql_server_2017_results")

### SQL Server before 2017

```
<span>SELECT</span> 
    fk.name <span>AS</span> foreign_key_name,
    t_parent.name <span>AS</span> table_name,
    c_parent.name <span>AS</span> column_name
<span>FROM</span> 
    sys.foreign_keys fk 
<span>INNER</span> <span>JOIN</span> sys.foreign_key_columns fkc <span>ON</span> 
    fkc.constraint_object_id = fk.object_id
<span>INNER</span> <span>JOIN</span> sys.tables t_parent <span>ON</span> 
    t_parent.object_id = fk.parent_object_id
<span>INNER</span> <span>JOIN</span> sys.columns c_parent <span>ON</span> 
    fkc.parent_column_id = c_parent.column_id  
    <span>AND</span> 
    c_parent.object_id = t_parent.object_id 
<span>LEFT</span> <span>JOIN</span> sys.index_columns idx_parent <span>ON</span>
    t_parent.object_id = idx_parent.object_id
    <span>AND</span>
    c_parent.column_id = idx_parent.column_id
<span>WHERE</span>
    idx_parent.index_column_id <span>IS</span> <span>NULL</span>
```

**Sample results**

![sql_server_2017_below_results](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-all-foreign-keys-without-an-index-in-sql-server-database/cd77d4876dc3e24e2ec055084c3d147f.png#center "sql_server_2017_below_results")