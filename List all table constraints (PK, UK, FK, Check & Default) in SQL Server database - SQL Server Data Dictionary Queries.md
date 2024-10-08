Query below lists all table (and view) constraints - primary keys, unique key constraints and indexes, foreign keys and check and default constraints.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> table_view,
    object_type, 
    constraint_type,
    constraint_name,
    details
<span>from</span> (
    <span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>] <span>as</span> table_view, 
        <span>case</span> <span>when</span> t.[<span>type</span>] = <span>'U'</span> <span>then</span> <span>'Table'</span>
            <span>when</span> t.[<span>type</span>] = <span>'V'</span> <span>then</span> <span>'View'</span>
            <span>end</span> <span>as</span> [object_type],
        <span>case</span> <span>when</span> c.[<span>type</span>] = <span>'PK'</span> <span>then</span> <span>'Primary key'</span>
            <span>when</span> c.[<span>type</span>] = <span>'UQ'</span> <span>then</span> <span>'Unique constraint'</span>
            <span>when</span> i.[<span>type</span>] = <span>1</span> <span>then</span> <span>'Unique clustered index'</span>
            <span>when</span> i.type = <span>2</span> <span>then</span> <span>'Unique index'</span>
            <span>end</span> <span>as</span> constraint_type, 
        <span>isnull</span>(c.[<span>name</span>], i.[<span>name</span>]) <span>as</span> constraint_name,
        <span>substring</span>(column_names, <span>1</span>, <span>len</span>(column_names)<span>-1</span>) <span>as</span> [details]
    <span>from</span> sys.objects t
        <span>left</span> <span>outer</span> <span>join</span> sys.indexes i
            <span>on</span> t.object_id = i.object_id
        <span>left</span> <span>outer</span> <span>join</span> sys.key_constraints c
            <span>on</span> i.object_id = c.parent_object_id 
            <span>and</span> i.index_id = c.unique_index_id
       <span>cross</span> <span>apply</span> (<span>select</span> col.[<span>name</span>] + <span>', '</span>
                        <span>from</span> sys.index_columns ic
                            <span>inner</span> <span>join</span> sys.columns <span>col</span>
                                <span>on</span> ic.object_id = col.object_id
                                <span>and</span> ic.column_id = col.column_id
                        <span>where</span> ic.object_id = t.object_id
                            <span>and</span> ic.index_id = i.index_id
                                <span>order</span> <span>by</span> col.column_id
                                <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) D (column_names)
    <span>where</span> is_unique = <span>1</span>
    <span>and</span> t.is_ms_shipped &lt;&gt; <span>1</span>
    <span>union</span> all 
    <span>select</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
        <span>'Table'</span>,
        <span>'Foreign key'</span>,
        fk.name <span>as</span> fk_constraint_name,
        schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name
    <span>from</span> sys.foreign_keys fk
        <span>inner</span> <span>join</span> sys.tables fk_tab
            <span>on</span> fk_tab.object_id = fk.parent_object_id
        <span>inner</span> <span>join</span> sys.tables pk_tab
            <span>on</span> pk_tab.object_id = fk.referenced_object_id
        <span>inner</span> <span>join</span> sys.foreign_key_columns fk_cols
            <span>on</span> fk_cols.constraint_object_id = fk.object_id
    <span>union</span> all
    <span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>],
        <span>'Table'</span>,
        <span>'Check constraint'</span>,
        con.[<span>name</span>] <span>as</span> constraint_name,
        con.[definition]
    <span>from</span> sys.check_constraints con
        <span>left</span> <span>outer</span> <span>join</span> sys.objects t
            <span>on</span> con.parent_object_id = t.object_id
        <span>left</span> <span>outer</span> <span>join</span> sys.all_columns <span>col</span>
            <span>on</span> con.parent_column_id = col.column_id
            <span>and</span> con.parent_object_id = col.object_id
    <span>union</span> all
    <span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>],
        <span>'Table'</span>,
        <span>'Default constraint'</span>,
        con.[<span>name</span>],
        col.[<span>name</span>] + <span>' = '</span> + con.[definition]
    <span>from</span> sys.default_constraints con
        <span>left</span> <span>outer</span> <span>join</span> sys.objects t
            <span>on</span> con.parent_object_id = t.object_id
        <span>left</span> <span>outer</span> <span>join</span> sys.all_columns <span>col</span>
            <span>on</span> con.parent_column_id = col.column_id
            <span>and</span> con.parent_object_id = col.object_id) a
<span>order</span> <span>by</span> table_view, constraint_type, constraint_name
```

## Columns

-   **table\_view** - table or view schema and name
-   **object\_type** - object type:
    -   Table
    -   View
-   **constraint\_type** - type of constraint:
    -   Primary key
    -   Unique key
    -   Foregin key
    -   Check constraint
    -   Default constraint
-   **constraint\_name** - name of constraint or index
-   **details** - details of this constraint:
    -   Primary key - PK column(s)
    -   Unique key - UK column(s)
    -   Foregin key - parent table name
    -   Check constraint - check definition
    -   Default constraint - column name and default value definition

## Rows

-   **One row** represents one constraint: PK, UK, FK, Check, Default
-   **Scope of rows:** all constraints
-   **Ordered by** schema, table name, constraint type

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/table_constraints.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)