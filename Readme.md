# Local Govt Reporter REST API

## Get list of meetings

### Request

`GET /meetings/`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/
    
##### Optional Parameters

###### Jurisdiction
`GET /meetings/?jurisdiction={jurisdiction}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/?jurisdiction=KCMO
    
###### State
`GET /meetings/?state={state}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/?state=MO
    
###### County
`GET /meetings/?county={county}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/?county=Jackson
    
###### Meeting Type
`GET /meetings/?meetingType={meetingType}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/?meetingType=Public%20Works`
    
###### Start Date AND End Date (must be used together and in ISO 8601 format)
`GET /meetings/?startDate={YYYY-MM-DD}&endDate={YYYY-MM-DD}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/?startDate=2021-02-01&endDate=2021-03-01`
    
###### Tags
`GET /meetings/?tags={tag}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/?tags=Special%20Session`

## Get a specific meeting

### Request

`GET /meeting/{meetingID}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/meetings/KCMO-2021-03-03-KCTGA-Comprehensive-HIV-Prevention-&-Care-Plan

## Get a list of jurisdictions

### Request

`GET /jurisdictions/`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/jurisdictions/
    
##### Optional Parameters

###### State
`GET /jurisdictions/?state={state}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/jurisdictions/?state=KS   

###### County
`GET /jurisdictions/?county={county}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/jurisdictions/?county=Johnson     
    
## Get a list of tags

### Request

`GET /tags/`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/tags/
    
##### Optional Parameters

###### State
`GET /tags/?state={state}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/tags/?state=KS   

###### County
`GET /tags/?county={county}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/tags/?county=Johnson   
    
###### Jurisdiction
`GET /tags/?jurisdiction={jurisdiction}`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/tags/?jurisdiction=Mission  

## Get a list of counties

### Request

`GET /counties/`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/counties/
    
## Get a list of states

### Request

`GET /states/`

    curl -i -H 'Accept: application/json' https://jt5wf041v4.execute-api.us-east-2.amazonaws.com/Prod/api/states/
   
