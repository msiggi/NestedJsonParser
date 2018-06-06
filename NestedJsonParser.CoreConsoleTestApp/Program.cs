using System;

namespace NestedJsonParser.CoreConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
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


            Console.WriteLine("Ready!");
            Console.ReadKey();
        }
    }
}
