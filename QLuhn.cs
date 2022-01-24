// QLuhn v1.0 (c) 2022 Sensei (aka 'Q')
// Calculate the checksum using Luhn's algorithm.
//
// Usage:
// QLuhn <number> <multipliers>
// QLuhn <number> <multipliers> <code>
//
// Examples:
// QLuhn [credit-card-number-without-code] 21
// QLuhn [credit-card-number] 21 [code]
//
// Compilation:
// %SYSTEMROOT%\Microsoft.NET\Framework\v3.5\csc QLuhn.cs

using System;

public static class QLuhn {
   public static int Calculate( string number, string multipliers ) {
      int checksum = 0;
      for( int i = 0; i < number.Length; i++ ) {
         int digit = Int32.Parse( number.Substring( i, 1 ) );
         int multiplier = Int32.Parse( multipliers.Substring( i % multipliers.Length, 1 ) );
         int value = ( digit * multiplier ) % 10;
         if( true ) { // Some implementations have it false e.g. PESEL
            value += ( digit * multiplier ) / 10;
         }
         //value %= 10;
         checksum += value;
         checksum %= 10;
         //Console.WriteLine( "{0} {1} {2} {3}", digit, multiplier, value, checksum ); // Just for debugging..
      }
      checksum %= 10;
      checksum = 10 - checksum;
      checksum %= 10;
      return( checksum );
   }

   public static void Help() {
      Console.WriteLine( "QLuhn v1.0 (c) 2022 Sensei (aka 'Q')" );
      Console.WriteLine( "Calculate the checksum using Luhn\'s algorithm." );
      Console.WriteLine();
      Console.WriteLine( "Usage:" );
      Console.WriteLine( "QLuhn <number> <multipliers>" );
      Console.WriteLine( "QLuhn <number> <multipliers> <code>" );
   }

   public static void Main( string [] args ) {
      try {
         if( args.Length == 2 ) {
            string number = args[ args.Length - 2 ];
            string multipliers = args[ args.Length - 1 ];
            int checksum = Calculate( number, multipliers );
            Console.WriteLine( checksum );
            System.Environment.Exit( 0 );
         } else if( args.Length == 3 ) {
            string number = args[ args.Length - 3 ];
            string multipliers = args[ args.Length - 2 ];
            int checksum_to_verify = Int32.Parse( args[ args.Length - 1 ] );
            int checksum = Calculate( number, multipliers );
            if( checksum == checksum_to_verify ) {
               Console.WriteLine( "1" );
            } else {
               Console.WriteLine( "0" );
            }
            System.Environment.Exit( 0 );
         } else {
            Help();
         }
      } catch( Exception e ) {
         Console.Error.WriteLine( e.Message );
         System.Environment.Exit( 20 );
      }
   }
}
