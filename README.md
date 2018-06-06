# NestedJsonParser
Parses unkown JSON to a &lt;name, value, type, childs...> - List. Implemented as Extension-Method.

Also available as Nuget-Package: 

https://www.nuget.org/packages/NestedJsonParser/

## Usage

```csharp
string json = @"{
            'id': 1,
            'location': {
                    'address': {
                        'city': 'Berlin',
                        'Street': 'Longstreet'
                    }
                },
            'type': 'donut',
	        'name': 'Cake',
            'ppu': 0.55,
            'testWithNestedObject': {
                        'nestedName': 'msiggi',
                        'nestedBand': 'immerhin',
                        },
	        'batters':
		        {
                    'batter':
                    [
				        { 'id': '1001', 'type': 'Regular' },
				        { 'id': '1002', 'type': 'Chocolate' },
				        { 'id': '1003', 'type': 'Blueberry' },
				        { 'id': '1004', 'type': 'Devils Food' }
			        ]
		            }
            }";

            var parsed = json.ToRawValueList();
            var flat = json.ToRawValueFlatList();
```

Inline-style: 
![Screenshot](ReadmeImages/Screenshot.png "")
