# Blackbird.io XTRF

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

XTRF is a comprehensive translation management system that streamlines and automates translation workflows for language service providers and corporate translation departments. It offers robust project management, collaboration, and automation features to enhance the efficiency of translation processes.

## Actions

### Classic - Job 

- **Get job** Get information about job
- **Update job** Update a job, specifying only the fields that require updating
- **Upload output file for job** Upload an output file for a job

### Classic - Project 

- **Create language combination for project** Create a new language combination for a classic project without creating a task
- **Create new project** Create a new classic project
- **Delete project** Delete a classic project.
- **Download file** Download the content of a specific file in a classic project
- **Get checkbox custom field for project** Retrieve a checkbox (boolean) custom field for a classic project
- **Get date custom field for project** Retrieve a date/date and time custom field for a classic project
- **Get multiple selection custom field for project** Retrieve a multiple selection (list) custom field for a classic project
- **Get number custom field for project** Retrieve a number custom field for a classic project
- **Get project details** Get information about classic project
- **Get text or selection custom field for project** Retrieve a text or selection custom field for a classic project
- **List custom fields for project** List the custom fields for a classic project, returning fields that can be assigned to the project without their values. To obtain the field value, use the "Get custom field for project" action corresponding to the field type
- **Update checkbox custom field for project** Update a checkbox (boolean) custom field for a classic project
- **Update date custom field for project** Update a date/date and time custom field for a classic project
- **Update multiple selection custom field for project** Update a multiple selection (list) custom field for a classic project
- **Update number custom field for project** Update a number custom field for a classic project
- **Update project** Update a classic project, specifying only the fields that require updating
- **Update text or selection custom field for project** Update a text or selection custom field for a classic project
- **Create payable for project** Create a payable for a classic project
- **Create receivable for project** Create a receivable for a classic project

### Classic - Quote

- **Create language combination for quote** Create a new language combination for a classic quote without creating a task
- **Delete quote** Delete a quote
- **Get checkbox custom field for quote** Retrieve a checkbox (boolean) custom field for a classic quote
- **Get date custom field for quote** Retrieve a date/date and time custom field for a classic quote
- **Get multiple selection custom field for quote** Retrieve a multiple selection (list) custom field for a classic quote
- **Get number custom field for quote** Retrieve a number custom field for a classic quote
- **Get quote** Get information about classic quote
- **Get text or selection custom field for quote** Retrieve a text or selection custom field for a classic quote
- **List custom fields for quote** List the custom fields for a classic quote, returning fields that can be assigned to the quote without their values. To obtain the field value, use the "Get custom field for quote" action corresponding to the field type
- **Send quote for customer confirmation** Send a quote for customer confirmation, changing the quote status to "Sent"
- **Start quote** Start a classic quote
- **Update checkbox custom field for quote** Update a checkbox (boolean) custom field for a classic quote
- **Update date custom field for quote** Update a date/date and time custom field for a classic quote
- **Update multiple selection custom field for quote** Update a multiple selection (list) custom field for a classic quote
- **Update number custom field for quote** Update a number custom field for a classic quote
- **Update quote instructions** Update instructions of a given quote, specifying only the fields that require updating
- **Update text or selection custom field for quote** Update a text or selection custom field for a classic quote
- **Create quote** Create a new classic quote based on provided data

### Classic - Task

- **Create task for project** Creates a new task for a given classic project
- **Create task for quote** Creates a new task for a given classic quote
- **Delete task** Delete a task
- **Get task in project** Retrieve information about a task within the project
- **Get task in quote** Retrieve information about a task within the quote
- **Get task progress** Get progress of a given task which contains information about task's status (ie. opened or ready) and current phase (ie. translation)
- **List task's files** List input (workfiles, translation memory, terminology, reference and log files) and output files
- **Start task** Start a task.
- **Update task in project** Update a task within the project, specifying only the fields that require updating
- **Update task in quote** Update a task within the quote, specifying only the fields that require updating
- **Upload file for task** Upload a file for a task.

### Customer

- **Create customer contact** Create a new contact person for a customer
- **Create customer** Create a new customer
- **Delete customer** Delete specific customer
- **Get checkbox custom field for customer** Retrieve a checkbox (boolean) custom field for a customer
- **Get customer details** Get information about specific customer
- **Get customers** Get all customers on this XTRF instance
- **Get date custom field for customer** Retrieve a date/date and time custom field for a customer
- **Get multiple selection custom field for customer** Retrieve a multiple selection (list) custom field for a customer
- **Get number custom field for customer** Retrieve a number custom field for a customer
- **Get text or selection custom field for customer** Retrieve a text or selection custom field for a customer
- **List custom fields for customer** List the custom fields for a customer, returning fields that can be assigned to the customer without their values. To obtain the field value, use the "Get custom field for customer" action corresponding to the field type
- **Update checkbox custom field for customer** Update a checkbox (boolean) custom field for a customer
- **Update customer contact** Update a contact person for a customer, specifying only the fields that require updating
- **Update customer** Update a customer, specifying only the fields that require updating
- **Update date custom field for customer** Update a date/date and time custom field for a customer
- **Update multiple selection custom field for customer** Update a multiple selection (list) custom field for a customer
- **Update number custom field for customer** Update a number custom field for a customer
- **Update text or selection custom field for customer** Update a text or selection custom field for a customer

### Provider

- **Search providers** Search for providers based on the given criteria.
- **Get provider** Get information about a specific provider.
- **Delete provider** Delete the provider with the given ID.
- **Send invitation to provider** Send an invitation to the provider with the given ID.
- **Run macros** Run macros on the provider with the given ID.

### Provider invoice

- **Search provider invoices** Search for provider invoices based on the given criteria.
- **Get provider invoice** Get provider invoice by ID.
- **Create provider invoice** Create a provider invoice.
- **Update provider invoice** Update provider invoice by ID.
- **Send provider invoice** Send provider invoice by specified ID.
- **Download provider invoice** Generates provider invoice document (PDF).
- **Delete provider invoice** Delete provider invoice by ID.

### Customer Invoice

- **Search customer invoices** Search for customer invoices based on the given criteria.
- **Get customer invoice** Get customer invoice by ID.
- **Create customer invoice** Create a customer invoice.
- **Delete customer invoice** Delete customer invoice by ID.
- **Download customer invoice** Download customer invoice document (PDF) by ID.
- **Send reminder for customer invoice** Send a reminder for customer invoice by specified ID.
- **Duplicate customer invoice** Duplicate a customer invoice by specified ID.
- **Export customer invoice** Export customer invoice by ID to json file to be imported in external system.

### View

- **Get view values** Get information about a specific view.

### User

- **Get user details** Get user details given a user ID.

### Provider person

- **Search provider persons** Search for provider persons based on the given criteria.
- **Get provider person** Get provider person by ID.
- **Remove provider person** Remove provider person by ID.
- **Send invitation to provider person** Send provider person invitation by ID.

### Smart - File 

- **Download file** Download the content of a specific file
- **List deliverable files for source file** List files ready for delivery with the same name as given source file in a smart project

### Smart - Job

- **Get delivered files in job** Get delivered files in a smart job
- **Get job details** Get information about a smart job
- **Get reference files shared with job** Get reference files shared with a smart job
- **Get work files shared with job** Get work files shared with a smart job
- **Share file as reference file with job** Share a file as a reference file with a smart job. Both the file and the job must belong to the same project
- **Share file as work file with job** Share a file as a work file with a smart job. Both the file and the job must belong to the same project
- **Stop sharing file with job** Stop sharing a file with a smart job
- **Update job** Update a smart job, specifying only the fields that require updating
- **Upload delivered file to job** Upload a delivered file to a smart job


### Smart - Project

- **Add target languages to project** Add more target languages to a smart project
- **Create new project** Create a new smart project
- **Delete payable for project** Delete a payable for a smart project
- **Delete receivable for project** Delete a receivable for a smart project
- **Get checkbox custom field for project** Retrieve a checkbox (boolean) custom field for a smart project
- **Get date custom field for project** Retrieve a date/date and time custom field for a smart project
- **Get multiple selection custom field for project** Retrieve a multiple selection (list) custom field for a smart project
- **Get number custom field for project** Retrieve a number custom field for a smart project
- **Get project details** Get information about a smart project. If you need to retrieve client contacts, finance information, process ID or check if project created in CAT tool or creation is queued, set the respective optional parameter to 'True'
- **Get project file details** Get information about specific file in a smart project
- **Get text or selection custom field for project** Retrieve a text or selection custom field for a smart project
- **List custom fields for project** List the custom fields for a smart project, returning fields that can be assigned to the project without their values. To obtain the field value, use the "Get custom field for project" action corresponding to the field type
- **List files in project** List all files in a smart project
- **List jobs in project** List all jobs in a smart project
- **Update checkbox custom field for project** Update a checkbox (boolean) custom field for a smart project
- **Update date custom field for project** Update a date/date and time custom field for a smart project
- **Update multiple selection custom field for project** Update a multiple selection (list) custom field for a smart project
- **Update number custom field for project** Update a number custom field for a smart project
- **Update project** Update a smart project, specifying only the fields that require updating
- **Update text or selection custom field for project** Update a text or selection custom field for a smart project
- **Upload file to project** Upload a file to a smart project
- **Create receivable for project** Create a receivable for a smart project
- **Create payable for project** Create a payable for a smart project

### Smart - Quote

- **Add target languages to quote** Add more target languages to a smart quote
- **Create new quote** Create a new smart quote
- **Add receivable to quote** Add a receivable to a smart quote
- **Delete payable for quote** Delete a payable for a smart quote
- **Delete receivable for quote** Delete a receivable for a smart quote
- **Get checkbox custom field for quote** Retrieve a checkbox (boolean) custom field for a smart quote
- **Get date custom field for quote** Retrieve a date/date and time custom field for a smart quote
- **Get multiple selection custom field for quote** Retrieve a multiple selection (list) custom field for a smart quote
- **Get number custom field for quote** Retrieve a number custom field for a smart quote
- **Get quote details** Get information about a smart quote. If you need to retrieve finance information, set the respective optional parameter to 'True'
- **Get quote file details** Get information about specific file in a smart quote
- **Get text or selection custom field for quote** Retrieve a text or selection custom field for a smart quote
- **List custom fields for quote** List the custom fields for a smart quote, returning fields that can be assigned to the quote without their values. To obtain the field value, use the "Get custom field for quote" action corresponding to the field type
- **List files in quote** List all files in a smart quote
- **List jobs in quote** List all jobs in a smart quote
- **Update checkbox custom field for quote** Update a checkbox (boolean) custom field for a smart quote
- **Update date custom field for quote** Update a date/date and time custom field for a smart quote
- **Update multiple selection custom field for quote** Update a multiple selection (list) custom field for a smart quote
- **Update number custom field for quote** Update a number custom field for a smart quote
- **Update quote** Update a smart quote, specifying only the fields that require updating
- **Update text or selection custom field for quote** Update a text or selection custom field for a smart quote
- **Upload file to quote** Upload a file to a smart quote

## Events

- **On customer created** Triggered when a new XTRF customer is created
- **On customer updated** Triggered when a new XTRF customer is updated
- **On job status changed** Triggered when the status of an XTRF job is changed
- **On project created** Triggered when a new XTRF project is created
- **On project status changed** Triggered when the status of an XTRF project is changed
- **On quote created** Triggered when a new XTRF quote is created
- **On quote status changed** Triggered when the status of an XTRF quote is changed
- **On client invoices created** Triggered when new client invoices are created. Checks for new invoices based on specified interval
- **On client invoices status changed** Triggered when status of any client invoice has changed
- **On vendor invoices created** Triggered when new vendor invoices are created. Checks for new invoices based on specified interval
- **On vendor invoices status changed** Triggered when status of any vendor invoice has changed

## Using filter strings in events

**On project status changed**, **On quote status changed** and **On customer updated** events have an optional parameter called _Filter string_, enabling result filtering based on custom fields. Please note the following:

- Currently, only text and single selection custom fields are supported for filtering. Complex filtering (such as AND, OR, NOT etc.) is not supported.
- Custom fields should be referenced by their keys with ".,()' characters and spaces changed to underscores. For instance, to reference _My text field_ you should use _customFields.My_text_field_.

For example, to trigger a bird on project status changes with the custom field _My text field_ set to _Some value_, select the **On project status changed** event in the bird editor. Then, choose the optional input _Filter string_ and enter _customFields.My_text_field=Some value_:

![Filtering example](image/filtering_example.png)

## Receivable and payable for project

Our app allows you to import CAT files into XTRF using specific actions such as **Create receivable for project** and **Create payable for project** for both smart and classic projects. This feature enables you to seamlessly integrate CAT tools with XTRF, streamlining your translation workflow.

With this functionality, you can easily transfer CAT files between the Phrase and XTRF apps, ensuring smooth collaboration and efficient project management.

Here's an example to demonstrate how this feature works:

![phrase-xtrf-1](image/phrase-xtrf-1.png)

![phrase-xtrf-2](image/phrase-xtrf-2.png)

![phrase-xtrf-2](image/phrase-xtrf-3.png)

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
