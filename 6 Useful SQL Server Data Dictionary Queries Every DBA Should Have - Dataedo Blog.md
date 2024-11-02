This is a list of handy SQL queries to the SQL Server data dictionary. You can also find [100+ other useful queries here](https://dataedo.com/kb/query/sql-server).

## 1\. List of tables with number of rows and comments

This query returns list of tables in a database sorted by schema and table name with comments and number of rows in each table.

### Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> schema_name,
       tab.name <span>as</span> table_name, 
       tab.create_date <span>as</span> created,  
       tab.modify_date <span>as</span> last_modified, 
       p.rows <span>as</span> num_rows, 
       ep.value <span>as</span> comments 
  <span>from</span> sys.tables tab
       <span>inner</span> <span>join</span> (<span>select</span> <span>distinct</span> 
                          p.object_id,
                          <span>sum</span>(p.rows) <span>rows</span>
                     <span>from</span> sys.tables t
                          <span>inner</span> <span>join</span> sys.partitions p 
                              <span>on</span> p.object_id = t.object_id 
                    <span>group</span> <span>by</span> p.object_id,
                          p.index_id) p
            <span>on</span> p.object_id = tab.object_id
        <span>left</span> <span>join</span> sys.extended_properties ep 
            <span>on</span> tab.object_id = ep.major_id
           <span>and</span> ep.name = <span>'MS_Description'</span>
           <span>and</span> ep.minor_id = <span>0</span>
           <span>and</span> ep.class_desc = <span>'OBJECT_OR_COLUMN'</span>
  <span>order</span> <span>by</span> schema_name,
        table_name
```

### Rows

One row represents one table. All tables will be included.

### Columns

| Column | Meaning |
| --- | --- |
| SCHEMA\_NAME | Schema name. |
| TABLE\_NAME | Table name. |
| CREATED | Table creation date and time. |
| LAST\_MODIFIED | Table last modification date and time. |
| NUM\_ROWS | Number of rows in the table. |
| COMMENTS | Table comments. |

### Sample results

![](https://dataedo.com/asset/img/blog/sql_server_data_dictionary_tables_query_results.png)

## 2\. List of views with definition and comments

This query returns list of database views with their definition SQL and a comment.

### Query

```
<span>select</span> schema_name(v.schema_id) <span>as</span> schema_name,
       v.name <span>as</span> view_name,
       v.create_date <span>as</span> created,
       v.modify_date <span>as</span> last_modified,
       m.definition,
       ep.value <span>as</span> comments
  <span>from</span> sys.views v
       <span>left</span> <span>join</span> sys.extended_properties ep 
           <span>on</span> v.object_id = ep.major_id
          <span>and</span> ep.name = <span>'MS_Description'</span>
          <span>and</span> ep.minor_id = <span>0</span>
          <span>and</span> ep.class_desc = <span>'OBJECT_OR_COLUMN'</span>
       <span>inner</span> <span>join</span> sys.sql_modules m 
           <span>on</span> m.object_id = v.object_id
 <span>order</span> <span>by</span> schema_name,
          view_name
```

### Rows

One row represents one view.

### Columns

| Column | Meaning |
| --- | --- |
| SCHEMA\_NAME | Schema name. |
| VIEW\_NAME | View name. |
| CREATED | View creation date and time. |
| LAST\_MODIFIED | View last modification date and time. |
| DEFINITION | View definition (SQL query). |
| COMMENTS | View comments. |

### Sample results

![](https://dataedo.com/asset/img/blog/sql_server_data_dictionary_views_query_results.png)

### Catalog of SQL Server queries

Browse a catalog of free SQL queries to help you explore SQL Server database schema.

[Browse queries](https://dataedo.com/kb/query/sql-server)

[![](https://dataedo.com/asset/img/banners/cta/data_dictionary_query.png)](https://dataedo.com/kb/query/sql-server)

## 3\. Table columns details

This query returns list of tables and their columns with details.

### Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> schema_name,
       tab.name <span>as</span> table_name, 
       col.name <span>as</span> column_name, 
       t.name <span>as</span> data_type,    
       t.name + 
       <span>case</span> <span>when</span> t.is_user_defined = <span>0</span> <span>then</span> 
                 <span>isnull</span>(<span>'('</span> + 
                 <span>case</span> <span>when</span> t.name <span>in</span> (<span>'binary'</span>, <span>'char'</span>, <span>'nchar'</span>, 
                           <span>'varchar'</span>, <span>'nvarchar'</span>, <span>'varbinary'</span>) <span>then</span>
                           <span>case</span> col.max_length 
                                <span>when</span> <span>-1</span> <span>then</span> <span>'MAX'</span> 
                                <span>else</span> 
                                     <span>case</span> <span>when</span> t.name <span>in</span> (<span>'nchar'</span>, 
                                               <span>'nvarchar'</span>) <span>then</span>
                                               <span>cast</span>(col.max_length/<span>2</span> 
                                               <span>as</span> <span>varchar</span>(<span>4</span>)) 
                                          <span>else</span> <span>cast</span>(col.max_length 
                                               <span>as</span> <span>varchar</span>(<span>4</span>)) 
                                     <span>end</span>
                           <span>end</span>
                      <span>when</span> t.name <span>in</span> (<span>'datetime2'</span>, <span>'datetimeoffset'</span>, 
                           <span>'time'</span>) <span>then</span> 
                           <span>cast</span>(col.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                      <span>when</span> t.name <span>in</span> (<span>'decimal'</span>, <span>'numeric'</span>) <span>then</span>
                            <span>cast</span>(col.precision <span>as</span> <span>varchar</span>(<span>4</span>)) + <span>', '</span> +
                            <span>cast</span>(col.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                 <span>end</span> + <span>')'</span>, <span>''</span>)        
            <span>else</span> <span>':'</span> + 
                 (<span>select</span> c_t.name + 
                         <span>isnull</span>(<span>'('</span> + 
                         <span>case</span> <span>when</span> c_t.name <span>in</span> (<span>'binary'</span>, <span>'char'</span>, 
                                   <span>'nchar'</span>, <span>'varchar'</span>, <span>'nvarchar'</span>, 
                                   <span>'varbinary'</span>) <span>then</span> 
                                    <span>case</span> c.max_length 
                                         <span>when</span> <span>-1</span> <span>then</span> <span>'MAX'</span> 
                                         <span>else</span>   
                                              <span>case</span> <span>when</span> t.name <span>in</span> 
                                                        (<span>'nchar'</span>, 
                                                        <span>'nvarchar'</span>) <span>then</span> 
                                                        <span>cast</span>(c.max_length/<span>2</span>
                                                        <span>as</span> <span>varchar</span>(<span>4</span>))
                                                   <span>else</span> <span>cast</span>(c.max_length
                                                        <span>as</span> <span>varchar</span>(<span>4</span>))
                                              <span>end</span>
                                    <span>end</span>
                              <span>when</span> c_t.name <span>in</span> (<span>'datetime2'</span>, 
                                   <span>'datetimeoffset'</span>, <span>'time'</span>) <span>then</span> 
                                   <span>cast</span>(c.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                              <span>when</span> c_t.name <span>in</span> (<span>'decimal'</span>, <span>'numeric'</span>) <span>then</span>
                                   <span>cast</span>(c.precision <span>as</span> <span>varchar</span>(<span>4</span>)) + <span>', '</span> 
                                   + <span>cast</span>(c.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                         <span>end</span> + <span>')'</span>, <span>''</span>) 
                    <span>from</span> sys.columns <span>as</span> c
                         <span>inner</span> <span>join</span> sys.types <span>as</span> c_t 
                             <span>on</span> c.system_type_id = c_t.user_type_id
                   <span>where</span> c.object_id = col.object_id
                     <span>and</span> c.column_id = col.column_id
                     <span>and</span> c.user_type_id = col.user_type_id
                 )
        <span>end</span> <span>as</span> data_type_ext,
        <span>case</span> <span>when</span> col.is_nullable = <span>0</span> <span>then</span> <span>'N'</span> 
             <span>else</span> <span>'Y'</span> <span>end</span> <span>as</span> nullable,
        <span>case</span> <span>when</span> def.definition <span>is</span> <span>not</span> <span>null</span> <span>then</span> def.definition 
             <span>else</span> <span>''</span> <span>end</span> <span>as</span> default_value,
        <span>case</span> <span>when</span> pk.column_id <span>is</span> <span>not</span> <span>null</span> <span>then</span> <span>'PK'</span> 
             <span>else</span> <span>''</span> <span>end</span> <span>as</span> primary_key, 
        <span>case</span> <span>when</span> fk.parent_column_id <span>is</span> <span>not</span> <span>null</span> <span>then</span> <span>'FK'</span> 
             <span>else</span> <span>''</span> <span>end</span> <span>as</span> foreign_key, 
        <span>case</span> <span>when</span> uk.column_id <span>is</span> <span>not</span> <span>null</span> <span>then</span> <span>'UK'</span> 
             <span>else</span> <span>''</span> <span>end</span> <span>as</span> unique_key,
        <span>case</span> <span>when</span> ch.check_const <span>is</span> <span>not</span> <span>null</span> <span>then</span> ch.check_const 
             <span>else</span> <span>''</span> <span>end</span> <span>as</span> check_contraint,
        cc.definition <span>as</span> computed_column_definition,
        ep.value <span>as</span> comments
   <span>from</span> sys.tables <span>as</span> tab
        <span>left</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
            <span>on</span> tab.object_id = col.object_id
        <span>left</span> <span>join</span> sys.types <span>as</span> t
            <span>on</span> col.user_type_id = t.user_type_id
        <span>left</span> <span>join</span> sys.default_constraints <span>as</span> <span>def</span>
            <span>on</span> def.object_id = col.default_object_id
        <span>left</span> <span>join</span> (
                  <span>select</span> index_columns.object_id, 
                         index_columns.column_id
                    <span>from</span> sys.index_columns
                         <span>inner</span> <span>join</span> sys.indexes 
                             <span>on</span> index_columns.object_id = indexes.object_id
                            <span>and</span> index_columns.index_id = indexes.index_id
                   <span>where</span> indexes.is_primary_key = <span>1</span>
                  ) <span>as</span> pk 
            <span>on</span> col.object_id = pk.object_id 
           <span>and</span> col.column_id = pk.column_id
        <span>left</span> <span>join</span> (
                  <span>select</span> fc.parent_column_id, 
                         fc.parent_object_id
                    <span>from</span> sys.foreign_keys <span>as</span> f 
                         <span>inner</span> <span>join</span> sys.foreign_key_columns <span>as</span> fc 
                             <span>on</span> f.object_id = fc.constraint_object_id
                   <span>group</span> <span>by</span> fc.parent_column_id, fc.parent_object_id
                  ) <span>as</span> fk
            <span>on</span> fk.parent_object_id = col.object_id 
           <span>and</span> fk.parent_column_id = col.column_id    
        <span>left</span> <span>join</span> (
                  <span>select</span> c.parent_column_id, 
                         c.parent_object_id, 
                         <span>'Check'</span> check_const
                    <span>from</span> sys.check_constraints <span>as</span> c
                   <span>group</span> <span>by</span> c.parent_column_id,
                         c.parent_object_id
                  ) <span>as</span> ch
            <span>on</span> col.column_id = ch.parent_column_id
           <span>and</span> col.object_id = ch.parent_object_id
        <span>left</span> <span>join</span> (
                  <span>select</span> index_columns.object_id, 
                         index_columns.column_id
                    <span>from</span> sys.index_columns
                         <span>inner</span> <span>join</span> sys.indexes 
                             <span>on</span> indexes.index_id = index_columns.index_id
                            <span>and</span> indexes.object_id = index_columns.object_id
                    <span>where</span> indexes.is_unique_constraint = <span>1</span>
                    <span>group</span> <span>by</span> index_columns.object_id, 
                          index_columns.column_id
                  ) <span>as</span> uk
            <span>on</span> col.column_id = uk.column_id 
           <span>and</span> col.object_id = uk.object_id
        <span>left</span> <span>join</span> sys.extended_properties <span>as</span> ep 
            <span>on</span> tab.object_id = ep.major_id
           <span>and</span> col.column_id = ep.minor_id
           <span>and</span> ep.name = <span>'MS_Description'</span>
           <span>and</span> ep.class_desc = <span>'OBJECT_OR_COLUMN'</span>
        <span>left</span> <span>join</span> sys.computed_columns <span>as</span> cc
            <span>on</span> tab.object_id = cc.object_id
           <span>and</span> col.column_id = cc.column_id
  <span>order</span> <span>by</span> schema_name,
        table_name, 
        column_name;   
```

### Rows

One row represents one table column.

### Columns

| Column | Meaning |
| --- | --- |
| SCHEMA\_NAME | Schema name. |
| TABLE\_NAME | Table name. |
| COLUMN\_NAME | Column name. |
| DATA\_TYPE | Data type. For instance, varchar or decimal. |
| DATA\_TYPE\_EXT | Data type with information about scale/precision or string length. For instance, varchar(100) or decimal(8, 2). |
| NULLABLE | Nullable flag. "Y" if column is nullable, "N" if column is not nullable. |
| DEFAULT\_VALUE | Column default value. |
| PRIMARY\_KEY | Primary key flag. "PK" when column is part of table primary key. |
| FOREIGN\_KEY | Foreign key flag. "FK" when column is part of foreign key. |
| UNIQUE\_KEY | Unique key flag. "UK" when column is part of unique key. |
| CHECK\_CONSTRAINT | Check constraint flag. "Check" when column is part of check constraint. |
| COMPUTED\_COLUMN\_DEFINITION | Computed column definition (not null only if column is computed). |
| COMMENTS | Column comments. |

### Sample results

![](https://dataedo.com/asset/img/blog/sql_server_data_dictionary_tables_columns_query_results.png)

## 4\. Foreign keys

This query returns list of tables and their foreign keys.

### Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> table_schema_name,
       tab.name <span>as</span> table_name,
       col.name <span>as</span> column_name,
       fk.name <span>as</span> constraint_name,
       schema_name(tab_prim.schema_id) <span>as</span> primary_table_schema_name,
       tab_prim.name <span>as</span> primary_table_name,
       col_prim.name <span>as</span> primary_table_column, 
       schema_name(tab.schema_id) + <span>'.'</span> + tab.name + <span>'.'</span> + 
            col.name + <span>' = '</span> + schema_name(tab_prim.schema_id) + <span>'.'</span> + 
            tab_prim.name + <span>'.'</span> + col_prim.name <span>as</span> join_condition,
       <span>case</span>
            <span>when</span> <span>count</span>(*) <span>over</span> (<span>partition</span> <span>by</span> fk.name) &gt; <span>1</span> <span>then</span> <span>'Y'</span>
            <span>else</span> <span>'N'</span>
       <span>end</span> <span>as</span> complex_fk,
       fkc.constraint_column_id <span>as</span> fk_part
  <span>from</span> sys.tables <span>as</span> tab
       <span>inner</span> <span>join</span> sys.foreign_keys <span>as</span> fk
           <span>on</span> tab.object_id = fk.parent_object_id
       <span>inner</span> <span>join</span> sys.foreign_key_columns <span>as</span> fkc
           <span>on</span> fk.object_id = fkc.constraint_object_id
       <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
           <span>on</span> fkc.parent_object_id = col.object_id
          <span>and</span> fkc.parent_column_id = col.column_id
       <span>inner</span> <span>join</span> sys.columns <span>as</span> col_prim
           <span>on</span> fkc.referenced_object_id = col_prim.object_id
          <span>and</span> fkc.referenced_column_id = col_prim.column_id
       <span>inner</span> <span>join</span> sys.tables <span>as</span> tab_prim
           <span>on</span> fk.referenced_object_id = tab_prim.object_id
 <span>order</span> <span>by</span> table_schema_name,
       table_name, 
       primary_table_name, 
       fk_part;
```

### Rows

One row represents one pair of columns in foreign key.

### Columns

| Column | Meaning |
| --- | --- |
| TABLE\_SCHEMA\_NAME | Foreign table schema name. |
| TABLE\_NAME | Foreign table name. |
| COLUMN\_NAME | Foreign table column name. |
| CONSTRAINT\_NAME | Constraint name. |
| PRIMARY\_TABLE\_SCHEMA\_NAME | Primary table schema name. |
| PRIMARY\_TABLE\_NAME | Primary table name. |
| PRIMARY\_TABLE\_COLUMN | Primary table column name. |
| JOIN\_CONDITION | Join condition containing foreign and primary key tables and columns. |
| COMPLEX\_FK | Complex foreign key flag. "Y" when foreign key is complex, otherwise "N". |
| FK\_PART | Represents part number of foreign key. |

### Sample results

![](https://dataedo.com/asset/img/blog/sql_server_data_dictionary_foreign_keys_query_results.png)

### Catalog of SQL Server queries

Browse a catalog of free SQL queries to help you explore SQL Server database schema.

[Browse queries](https://dataedo.com/kb/query/sql-server)

[![](https://dataedo.com/asset/img/banners/cta/data_dictionary_query.png)](https://dataedo.com/kb/query/sql-server)

## 5\. Views columns

This query returns list of views with their columns.

### Query

```
<span>select</span> schema_name(v.schema_id) <span>as</span> schema_name,
       v.name <span>as</span> view_name, 
       col.name <span>as</span> column_name,
       t.name <span>as</span> data_type,
       t.name + 
       <span>case</span> <span>when</span> t.is_user_defined = <span>0</span> <span>then</span> 
                 <span>isnull</span>(<span>'('</span> + 
                 <span>case</span> <span>when</span> t.name <span>in</span> (<span>'binary'</span>, <span>'char'</span>, <span>'nchar'</span>,
                           <span>'varchar'</span>, <span>'nvarchar'</span>, <span>'varbinary'</span>) <span>then</span>
                           <span>case</span> col.max_length 
                                <span>when</span> <span>-1</span> <span>then</span> <span>'MAX'</span> 
                                <span>else</span> 
                                     <span>case</span> 
                                         <span>when</span> t.name <span>in</span> (<span>'nchar'</span>, 
                                              <span>'nvarchar'</span>) <span>then</span>
                                              <span>cast</span>(col.max_length/<span>2</span> 
                                              <span>as</span> <span>varchar</span>(<span>4</span>))
                                         <span>else</span> <span>cast</span>(col.max_length 
                                              <span>as</span> <span>varchar</span>(<span>4</span>))
                                     <span>end</span>
                           <span>end</span>
                      <span>when</span> t.name <span>in</span> (<span>'datetime2'</span>, 
                           <span>'datetimeoffset'</span>, <span>'time'</span>) <span>then</span> 
                            <span>cast</span>(col.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                      <span>when</span> t.name <span>in</span> (<span>'decimal'</span>, <span>'numeric'</span>) <span>then</span> 
                           <span>cast</span>(col.precision <span>as</span> <span>varchar</span>(<span>4</span>)) + <span>', '</span> +
                           <span>cast</span>(col.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                 <span>end</span> + <span>')'</span>, <span>''</span>)        
            <span>else</span> <span>':'</span> +
                 (<span>select</span> c_t.name + 
                         <span>isnull</span>(<span>'('</span> + 
                         <span>case</span> <span>when</span> c_t.name <span>in</span> (<span>'binary'</span>, <span>'char'</span>,
                                   <span>'nchar'</span>, <span>'varchar'</span>, <span>'nvarchar'</span>,
                                   <span>'varbinary'</span>) <span>then</span>
                                   <span>case</span> c.max_length
                                        <span>when</span> <span>-1</span> <span>then</span> <span>'MAX'</span>
                                        <span>else</span> <span>case</span> <span>when</span> t.name <span>in</span>
                                                       (<span>'nchar'</span>,
                                                        <span>'nvarchar'</span>)
                                                  <span>then</span> <span>cast</span>(c.max_length/<span>2</span>
                                                       <span>as</span> <span>varchar</span>(<span>4</span>))
                                                  <span>else</span> <span>cast</span>(c.max_length
                                                       <span>as</span> <span>varchar</span>(<span>4</span>))
                                             <span>end</span>
                                   <span>end</span>
                              <span>when</span> c_t.name <span>in</span> (<span>'datetime2'</span>, 
                                   <span>'datetimeoffset'</span>, <span>'time'</span>) <span>then</span>
                                   <span>cast</span>(c.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                              <span>when</span> c_t.name <span>in</span> (<span>'decimal'</span>, <span>'numeric'</span>) <span>then</span>
                                   <span>cast</span>(c.precision <span>as</span> <span>varchar</span>(<span>4</span>)) +
                                   <span>', '</span> + <span>cast</span>(c.scale <span>as</span> <span>varchar</span>(<span>4</span>))
                         <span>end</span> + <span>')'</span>, <span>''</span>)
                    <span>from</span> sys.columns <span>as</span> c
                         <span>inner</span> <span>join</span> sys.types <span>as</span> c_t 
                             <span>on</span> c.system_type_id = c_t.user_type_id
                   <span>where</span> c.object_id = col.object_id
                     <span>and</span> c.column_id = col.column_id
                     <span>and</span> c.user_type_id = col.user_type_id
                 ) 
       <span>end</span> <span>as</span> data_type_ext,
       <span>case</span> <span>when</span> col.is_nullable = <span>0</span> <span>then</span> <span>'N'</span> <span>else</span> <span>'Y'</span> <span>end</span> <span>as</span> nullable,
       ep.value <span>as</span> comments
  <span>from</span> sys.views <span>as</span> v
       <span>join</span> sys.columns <span>as</span> <span>col</span>
           <span>on</span> v.object_id = col.object_id
       <span>left</span> <span>join</span> sys.types <span>as</span> t
           <span>on</span> col.user_type_id = t.user_type_id
       <span>left</span> <span>join</span> sys.extended_properties <span>as</span> ep 
           <span>on</span> v.object_id = ep.major_id
          <span>and</span> col.column_id = ep.minor_id
          <span>and</span> ep.name = <span>'MS_Description'</span>        
          <span>and</span> ep.class_desc = <span>'OBJECT_OR_COLUMN'</span>
 <span>order</span> <span>by</span> schema_name,
       view_name,
       column_name;
```

### Rows

One row represents one view column.

### Columns

| Column | Meaning |
| --- | --- |
| SCHEMA\_NAME | Schema name. |
| VIEW\_NAME | View name. |
| COLUMN\_NAME | View column name. |
| DATA\_TYPE | Data type. For instance, varchar or decimal. |
| DATA\_TYPE\_EXT | Data type with information about scale/precision or string length. For instance, varchar(100) or decimal(8, 2). |
| NULLABLE | Nullable flag. "Y" if column is nullable, "N" if column is not nullable. |
| COMMENTS | Column comments. |

### Sample results

![](https://dataedo.com/asset/img/blog/sql_server_data_dictionary_views_columns_query_results.png)

## 6\. Tables by number of columns

This query returns list of tables sorted by the number of columns they contain.

### Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> schema_name, 
       tab.name <span>as</span> table_name, 
       <span>count</span>(*) <span>as</span> <span>columns</span>
  <span>from</span> sys.tables <span>as</span> tab
       <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
           <span>on</span> tab.object_id = col.object_id 
 <span>group</span> <span>by</span> schema_name(tab.schema_id), 
       tab.name
 <span>order</span> <span>by</span> <span>count</span>(*) <span>desc</span>;
```

### Rows

One row represents one user table.

### Columns

| Column | Meaning |
| --- | --- |
| SCHEMA\_NAME | Schema name. |
| TABLE\_NAME | Table name. |
| COLUMNS | Number of columns table contains. |

### Sample results

![](https://dataedo.com/asset/img/blog/sql_server_data_dictionary_tables_by_columns_results.png)

### Extract and share data dictionary from SQL Server

Read data dictionary, generate interactive HTML documentation and allow your team to easily discover schema in your SQL Server databases.

[Try Free Now](https://dataedo.com/free-trial)

[![](https://dataedo.com/asset/img/banners/cta/documentation.png)](https://dataedo.com/free-trial)