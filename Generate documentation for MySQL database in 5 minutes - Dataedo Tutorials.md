This tutorial will teach you how to quickly generate documentation for your **MySQL** database with **Dataedo** tool. It will create and export to PDF or HTML a [Data Dictionary](https://dataedo.com/blog/what-is-data-dictionary) of your database.

## 1\. Prepare

1.  [Download Dataedo](https://dataedo.com/download-dataedo)
2.  Install it
3.  [Get free trial key (optional)](https://dataedo.com/free-trial?cta=tdocmy1), if you don't have it already, to unlock export to HTML, ER diagrams and more.

## 2\. Create file repository

When you start the application you will see this window. To create a file with your first documentation double click the **Create new repository** option.

![Welcome form](https://dataedo.com/asset/img/tutorials/8_0/new_repo_window.png)

Next, double click the **In a file** option.

![Welcome form](https://dataedo.com/asset/img/tutorials/8_0/new_file_window.png)

Choose a location and a file name and click **Save**.

![New file - location and filename](https://dataedo.com/asset/img/tutorials/8_0/new_file_window_name.png)

You opened your new file and can use **repository explorer** to navigate sample documentations.

![Dataedo editor](https://dataedo.com/asset/img/tutorials/8_0/dataedo_first_view.png)

## 3\. Connect to your database

Now it is time to connect to your MySQL database and import schema. On the ribbon click **Add documentation** and **Database connection**.

![Add documentation button](https://dataedo.com/asset/img/tutorials/8_0/add_documntation_button.png)

Now you need to provide connection details. First, choose "MySQL" from **DBMS** dropdown, then fill the following details:

1.  **Host** - name or IP of your host/server
2.  **Port** - change the default port of MySQL instance if required
3.  **User** - provide your username
4.  **Password** - provide your password
5.  **Save password** (optional) - you can save your password for future updates, when your schema changes and you want to reflect that in your documentation
6.  **Database** - type in name of the database or choose from the list (click \[...\] button)

Click **Connect** when ready.

![](https://dataedo.com/asset/img/docs/8_0/mysql_connection_form.png)

Here is a comparison with connection details in **MySQL Workbench**.

![Connection to SQL Server](https://dataedo.com/asset/img/docs/7_0/mysql_connection_workbench1.png)

Dataedo lists objects found in the database. Click **Next**.

![](https://dataedo.com/asset/img/blog/tutorials/8_0/connect_select.png)

Now you can change default name (you can always change it in repository explorer). Click **Import** to start import. Dataedo now imports schema details. When done, close with **Finish**.

![](https://dataedo.com/asset/img/tutorials/8_0/connect_documentation_title.png)

## 4\. Documentation overview

You can browse repository explorer to view imported schema objects. You can see in convenient manner the following information:

1.  Table and column descriptions
2.  Table and column relationships (foreign keys)
3.  Table triggers
4.  Object dependencies

![](https://dataedo.com/asset/img/tutorials/8_0/dataedo_table_columns.png)

## 5\. Export to PDF

Now it's time to share the documentation with your colleagues. You can share .dataedo file or send a PDF export.

To export documentation to PDF select your documentation in repository explorer and click **Export documentation** button on the ribbon. Then choose **PDF** and first template and confirm with **Next**.

![](https://dataedo.com/asset/img/blog/tutorials/8_0/export_pdf_template.png)

In this step, you may choose to exclude certain elements from the export.

![](https://dataedo.com/asset/img/tutorials/8_0/export_object_types.png)

Now, choose a location and name of your file and finish with **Export**.

![](https://dataedo.com/asset/img/tutorials/8_0/export_pdf_filename.png)

When export is done, you will be prompted to open generated file.

![](https://dataedo.com/asset/img/tutorials/share_pdf.png)

[Download a sample PDF](https://dataedo.com/download/AdventureWorks.pdf)

## 6\. Export to HTML (Pro feature)

HTML is the advised export format - it's much more convenient to browse and search. If you haven't already, [get a free trial key](https://dataedo.com/free-trial?cta=bsqltut3) to unlock HTML export (and much more).

Choose **HTML** in **Export documentation** option and choose the right template:

1.  **Web server** – if you want to host it on a web server or open from disk with Firefox (for security reasons doesn't in Chrome and Edge)
2.  **Local disk** – Worse performance, but works locally in Chrome and Edge.

This is how HTML export looks like:

![](https://dataedo.com/asset/img/kb/db-tools/ssms/dataedo_html_fks.png)

[**See sample HTML**](https://dataedo.com/samples/html/AdventureWorks/doc/AdventureWorks_2/modules/People_8/tables/Person_Address_135.html)

### Generate Documentation for Your MySQL Database

Generate and share convenient HTML documentation of your MySQL databases in minutes.

[Try Free Now](https://dataedo.com/free-trial)

[![](https://dataedo.com/asset/img/banners/cta/documentation.png)](https://dataedo.com/free-trial)