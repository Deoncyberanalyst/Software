import requests
import base64
import sys

def get_base_branch_id(url, headers, base_branch):
    response = requests.get(url, headers=headers)
    if response.status_code == 200:
        responseData = response.json()
        for branch in responseData["value"]:
            if branch["name"] == f"refs/heads/{base_branch}":
                print(f"Found the base branch id {branch['objectId']}")
                return branch["objectId"]
    else:
        print(f"Failed to get the base branch id. Error: {response.status_code} {response.text}")
        return None

if __name__ == "__main__":
    organization = ""
    project = ""
    repository = ""
    new_branch_name = ""
    base_branch_name = ""

    url = f"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repository}/refs?api-version=7.1"
    pat = ""

    pat = f":{pat}"
    pat = base64.b64encode(pat.encode('utf-8'))
    
    # Set the request headers
    headers = {
        "Content-Type": "application/json",
        "Authorization": b"Basic " + pat
    }

    base_branch_id = get_base_branch_id(url, headers, base_branch_name)

    if base_branch_id == None:
        print("Failed to get the base branch id. Exiting...")
        sys.exit(1)

    data = [{
        "name": f"refs/heads/{new_branch_name}",
        "oldObjectId": "0000000000000000000000000000000000000000",
        "newObjectId": f"{base_branch_id}"   
    }]

# Send the POST request to create the branch
response = requests.post(url, headers=headers, json=data)

# Check the response status code
if response.status_code == 200:
    responseData = response.json()
    statusCode = responseData['value']
    for items in statusCode:
        if items['success'] == True: print(f"Branch created successfully")
        else: print(f"Error: {responseData}")

else:
    print(f"Error Response code: {response.status_code}")


