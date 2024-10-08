This is a collection of queries for **SQL Server** system catalog (data dictionary) that help you find information about foreign keys. If you are interested in schema and metadata analysis have a look at:

-   [More SQL Server queries](https://dataedo.com/kb/query/sql-server)
-   [More Database engines](https://dataedo.com/kb/query/)

## Basic queries

### 1\. Foreign keys: row per column

**One row** represents one foreign key column. If foreign key consists of multiple columns (composite key), each column appears separately.

```
<span>select</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table,
    fk_cols.constraint_column_id <span>as</span> <span>no</span>, 
    fk_col.name <span>as</span> fk_column_name,
    <span>' = '</span> <span>as</span> [<span>join</span>],
    pk_col.name <span>as</span> pk_column_name,
    fk.name <span>as</span> fk_constraint_name
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
    <span>inner</span> <span>join</span> sys.foreign_key_columns fk_cols
        <span>on</span> fk_cols.constraint_object_id = fk.object_id
    <span>inner</span> <span>join</span> sys.columns fk_col
        <span>on</span> fk_col.column_id = fk_cols.parent_column_id
        <span>and</span> fk_col.object_id = fk_tab.object_id
    <span>inner</span> <span>join</span> sys.columns pk_col
        <span>on</span> pk_col.column_id = fk_cols.referenced_column_id
        <span>and</span> pk_col.object_id = pk_tab.object_id
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name, 
    fk_cols.constraint_column_id
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/foreign_key_columns.png)

**Columns:**

-   **foreign\_table** - foreign table name with schema name
-   **rel** - relationship symbol implicating direction
-   **primary\_table** - primary (referenced) table name with schema name
-   **no** - id of the column in key. Single column keys always have 1, composite keys have 1, 2, ... n for each column of the key
-   **fk\_column\_name** - foreign table column
-   **join** - "=" symbol indicating join operation for pair of columns
-   **pk\_column\_name** - primary (referenced) table column
-   **fk\_constraint\_name** - foreign key constraint name

### 2\. Foreign keys: row per key

**One row** represents one foreign key. If foreign key consists of multiple columns (composite key) it is still represented as one row.

```
<span>select</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table,
    <span>substring</span>(column_names, <span>1</span>, <span>len</span>(column_names)<span>-1</span>) <span>as</span> [fk_columns],
    fk.name <span>as</span> fk_constraint_name
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
    <span>cross</span> <span>apply</span> (<span>select</span> col.[<span>name</span>] + <span>', '</span>
                    <span>from</span> sys.foreign_key_columns fk_c
                        <span>inner</span> <span>join</span> sys.columns <span>col</span>
                            <span>on</span> fk_c.parent_object_id = col.object_id
                            <span>and</span> fk_c.parent_column_id = col.column_id
                    <span>where</span> fk_c.parent_object_id = fk_tab.object_id
                      <span>and</span> fk_c.constraint_object_id = fk.object_id
                            <span>order</span> <span>by</span> col.column_id
                            <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) D (column_names)
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/foreign_keys.png)

**Columns:**

-   **foreign\_table** - foreign table name with schema name
-   **rel** - relationship symbol implicating direction
-   **primary\_table** - primary (referenced) table name with schema name
-   **fk\_columns** - list of FK column names, separated with ","
-   **fk\_constraint\_name** - foreign key constraint name

## Visualize foreign keys

Listing foreign keys is very useful, but it's also really beneficial to visualize foreign keys with ER diagrams (ERDs). [Dataedo](https://dataedo.com/product/entity-relationship-diagram-tool) allows you to import schema from your database, describe each field and visualize quickly with several small, manageable diagrams.

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-foreign-keys-sql-queries/9b5c10481c516584068af515672b0219.png#center "Image title")

Then, finally, you can share complete documentation, with diagrams and data dictionary in [HTML](https://dataedo.com/product/html-export), PDF or on prem [web portal](https://dataedo.com/product/web-catalog).

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-foreign-keys-sql-queries/45d55ddf75b8b2872165273c3c57be86.png#center "Image title")

[**Try it for free now**](https://dataedo.com/free-trial)

## More queries

### 3\. All columns and their FKs (if present)

Query below returns all columns from all tables in a database with a foreign key reference if column has one.

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>],
    col.column_id,
    col.name <span>as</span> column_name,
    <span>case</span> <span>when</span> fk.object_id <span>is</span> <span>not</span> <span>null</span> <span>then</span> <span>'&gt;-'</span> <span>else</span> <span>null</span> <span>end</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table,
    pk_col.name <span>as</span> pk_column_name,
    fk_cols.constraint_column_id <span>as</span> <span>no</span>,
    fk.name <span>as</span> fk_constraint_name
<span>from</span> sys.tables tab
    <span>inner</span> <span>join</span> sys.columns <span>col</span> 
        <span>on</span> col.object_id = tab.object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.foreign_key_columns fk_cols
        <span>on</span> fk_cols.parent_object_id = tab.object_id
        <span>and</span> fk_cols.parent_column_id = col.column_id
    <span>left</span> <span>outer</span> <span>join</span> sys.foreign_keys fk
        <span>on</span> fk.object_id = fk_cols.constraint_object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk_cols.referenced_object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.columns pk_col
        <span>on</span> pk_col.column_id = fk_cols.referenced_column_id
        <span>and</span> pk_col.object_id = fk_cols.referenced_object_id
<span>order</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name,
    col.column_id
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/columns_with_fks.png)

**Columns:**

-   **table** - table in a database with schema name
-   **column\_id** - number of column in a database
-   **column\_name** - column name
-   **rel** - relationship symbol ('>-') indicating foreign key and direction
-   **primary\_table** - referenced table
-   **pk\_column\_name** - referenced column
-   **no** - column id in a key constraint
-   **fk\_constraint\_name** - foreign key constraint name

### 4\. All tables referenced by specific table

Query below lists all tables referenced with foreign key by specific table.

**Please note:** There can be more tables with the same name. If that's the case, uncomment where clause and provide schema name.

```
<span>select</span> <span>distinct</span> 
    schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
<span>where</span> fk_tab.[<span>name</span>] = <span>'Your table'</span> <span>-- enter table name here</span>
<span>--  and schema_name(fk_tab.schema_id) = 'Your table schema name'</span>
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/referenced_tables.png)

**Columns:**

-   **foreign\_table** - foreign table name with schema name - the table you provided as a parameter
-   **foreign\_table** - relationship symbol implicating FK and direction
-   **primary\_table** - primary (referenced) tables names with schema name - the tables you are looking for

### 5\. All tables referencing specific table

Query below lists all tables that reference specific table with foreign keys.

**Please note:** There can be more tables with the same name. If that's the case, uncomment where clause and provide schema name.

```
<span>select</span> <span>distinct</span> 
    schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
<span>where</span> pk_tab.[<span>name</span>] = <span>'Your table'</span> <span>-- enter table name here</span>
<span>--  and schema_name(pk_tab.schema_id) = 'Your table schema name'</span>
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/referencing_tables.png)

**Columns:**

-   **foreign\_table** - foreign tables schemas and names - the table you are looking for
-   **foreign\_table** - relationship symbol implicating FK and direction
-   **primary\_table** - primary (referenced) tables names with schema name - the table you provided as a parameter

## What if foreign key constraints are note defined in your database?

[Many databases](https://dataedo.com/blog/dont-most-databases-have-foreign-keys-constraints), even [major applications from Microsoft, Oracle or SAP](https://dataedo.com/blog/major-applications-do-not-use-foreign-key-constraints-in-their-databases-oracle-microsoft-sap) **do not have foreign key constraints** in their schema. That means you can't discover relationships with queries in this article. There are [many reasons for why that is](https://dataedo.com/blog/why-there-are-no-foreign-keys-in-your-database-referential-integrity-checks).

What if your database looked more like this?

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-foreign-keys-sql-queries/4737eac592cb494ac3ad319a2a37d264.png#center "Image title")

Well, in that case you can always **document** foreign keys in your database with [Dataedo](https://dataedo.com/product/data-dictionary-tool). Simply connect to your database, import schema to local repository and define relationships (but also, keys, descriptions, aliases, and more). That will give you (and everyone using or designing the database) reference documentation, and you will be able to create ER diagrams, too.

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-foreign-keys-sql-queries/93b0452d8b0a381fb2144546791a7786.png#center "Image title")

![Image title](https://dataedo.com/asset/img/markdown/kb/metadata/sql-server/list-foreign-keys-sql-queries/bf62d9e0214e97beb20bb13fc1b52c86.png#center "Image title")

[**Try it for free now**](https://dataedo.com/free-trial)

## Statistical queries

### 6\. Most referenced tables

Query below lists tables that are most referenced by other tables with foreign keys. Those are the dictionary tables such as **person**, **product** or **store**. In data warehouses those are dimension tables.

![](https://dataedo.com/asset/img/kb/query/referenced_table.png)

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>],
   <span>count</span>(fk.name) <span>as</span> [<span>references</span>],
   <span>count</span>(<span>distinct</span> fk.parent_object_id) <span>as</span> referencing_tables
<span>from</span> sys.tables <span>as</span> tab
   <span>left</span> <span>join</span> sys.foreign_keys <span>as</span> fk
       <span>on</span> tab.object_id = fk.referenced_object_id
<span>group</span> <span>by</span> schema_name(tab.schema_id), tab.name
<span>having</span> <span>count</span>(fk.name) &gt; <span>0</span>
<span>order</span> <span>by</span> <span>2</span> <span>desc</span>
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/most_referenced_tables.png)

**Columns:**

-   **table** - name of the table with schema name
-   **references** - number of foreign keys referencing to this table
-   **referencing\_tables** - number of different tables referencing to this table

### 7\. Tables with most FKs

Query below lists tables with their number of foreign keys and number of tables they refer to.

![](https://dataedo.com/asset/img/kb/query/table_foreign_keys.png)

```
<span>select</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> [<span>table</span>],
    <span>count</span>(*) foreign_keys,
    <span>count</span> (<span>distinct</span> referenced_object_id) referenced_tables
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
<span>group</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name
<span>order</span> <span>by</span> <span>count</span>(*) <span>desc</span>
```

**Sample results:**

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_by_number_of_fks.png)

**Columns:**

-   **table** - table with schema name
-   **foreign\_keys** - number of foreign keys in a table
-   **referenced\_tables** - number of referenced tables. Note that it is not the same as number of foreign keys, as multiple foreign keys may reference the same table.

## Create documentation of your databases in minutes

You can get interactive HTML documetation of your database in a couple of minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[**Try it for free now**](http://dataedo.com/free-trial)