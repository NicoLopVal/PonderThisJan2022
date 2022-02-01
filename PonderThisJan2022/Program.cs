//int numberSizeD = 5;
//int circleSizeN = 7;
int numberSizeD = 6;
int circleSizeN = 8;

//char[] circle = { '1', '3', '7', '9', '2', '4', '5'};     //MAX
//char[] circle = { '0', '2', '4', '6', '8', '5', '1' };    //MIN
//char[] circle = { '4', '7', '3', '6', '2', '0', '1' };    //Sample

//char[] circle = { '1', '3', '7', '9', '8', '4', '5', '6'};     //MAX for bonus
char[] circle = { '0', '2', '4', '6', '8', '5', '7', '9' }; //MIN for bonus

// Get the biggest posible combination of numbers to find all primes until that value
string ascendingNumbers = "9876543210";
string  maxNumber = ascendingNumbers.Substring(0, numberSizeD);

// Get all the prime numbers below the max number
List<double> allPrimes = new List<double>();
allPrimes.Add(2);
for (double i = 3; i <= Convert.ToDouble(maxNumber); i++)
{
    bool isPrime = true;
    foreach(double prime in allPrimes)
    {
        if(i%prime == 0)
        {
            isPrime = false;
            break;  
        }
    }
    if(isPrime)
        allPrimes.Add(i);
}

//Circle modifiyer to look for the answer
List<double> numberPerm = new List<double>();
List<string> circlePermutations = new List<string>();
List<double> primePermutationList = GetPrimePermutations(circle, numberSizeD, circleSizeN, numberPerm);

GetPermutations(circle, 2, circleSizeN, circleSizeN);
//maxValue = 0;
double maxTotalValue = 0;
double minTotalValue = 10000;
string maxSelectedValue = "";
string minSelectedValue = "";

foreach(string combination in circlePermutations)
{
    char[] circleDou2Char = new char[combination.ToString().Length];
    circleDou2Char = combination.ToString().ToCharArray();
    double circleScore = CircleResult(allPrimes, primePermutationList, circleDou2Char);

    if (circleScore > maxTotalValue)
    {
        maxTotalValue = circleScore;
        maxSelectedValue = combination;
    }

    if (circleScore < minTotalValue)
    {
        minTotalValue = circleScore;
        minSelectedValue = combination;
    }

}

//RESULT
Console.WriteLine("The circle with highest score is: {0} with a total of: {1}", maxSelectedValue ,maxTotalValue);
Console.WriteLine("The circle with lowest score is: {0} with a total of: {1}", minSelectedValue, minTotalValue);


// Get the value of the circles
double CircleResult(List<double> allPrimes, List<double> permutations, char[] thisCircle)
{
    double result = 0;

    foreach(double permute in permutations)
    {
        if (allPrimes.Contains(permute))
        {
            result = result + CheckValue(thisCircle,permute.ToString());
        }
    }

    return result;
}

// Get all permutations for a specific array, credits to Peter and nevvermind from StackOverflow
void Swap(ref char a, ref char b)
{
    if (a == b) return;

    var temp = a;
    a = b;
    b = temp;
}

void GetPermutations(char[] list, int type, int numSizeD, int circleTamN)
{
    int x = list.Length - 1;
    GetPer(list, 0, x, type, numSizeD, circleTamN);
}

void GetPer(char[] list, int k, int m, int type, int numSizeD, int circleTamN)
{
    if (k == m)
    {
        // Removing permutations that won't be primes
        if (type == 1 && (!(list.Last() == '0') && !(list.Last() == '2') && !(list.Last() == '4') && !(list.Last() == '6') && !(list.Last() == '8') && list[circleSizeN - numberSizeD] != '0'))
            numberPerm.Add(Convert.ToDouble((new String(list)).Substring(circleTamN - numSizeD)));
        else if (type == 2)
            circlePermutations.Add((new String(list)));
    }
    else
        for (int i = k; i <= m; i++)
        {
            Swap(ref list[k], ref list[i]);
            GetPer(list, k + 1, m, type, numSizeD, circleTamN);
            Swap(ref list[k], ref list[i]);
        }
}


// Get the tootal value of a number in a specified circle
int CheckValue(char[] circleDigits, string value) 
{
    int sum = 0;
    int counter = 0;
    string[] circleDigString = new string[circleDigits.Length];


    foreach (char c in circleDigits)
    {
        circleDigString[counter] = c.ToString();
        counter++;
    }

    for (int i = 0; i < value.Length-1; i++)
    {
        int indexOne = Array.IndexOf(circleDigString, value[i].ToString());
        int indexTwo = Array.IndexOf(circleDigString, value[i+1].ToString());
        sum = sum + Math.Min(Math.Abs(indexOne - indexTwo), Math.Min(indexOne, indexTwo) + circleDigits.Length - Math.Max(indexOne, indexTwo));
    }
    return sum;
}

List<double> GetPrimePermutations(char[] circle, int numberSizeD, int circleSizeN, List<double> Permutations)
{
    List<double> primePermutations = new List<double>();

    GetPermutations(circle, 1, numberSizeD, circleSizeN);
    Permutations = Permutations.Distinct().ToList();
    foreach (double perm in Permutations)
    {
        if (allPrimes.Contains(perm))
            primePermutations.Add(perm);
    }

    return primePermutations;
}
