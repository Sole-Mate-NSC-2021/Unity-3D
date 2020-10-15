#include<bits/stdc++.h>
using namespace std;
int main()
{
int n,l,w,i,sum=0;
scanf("%d",&n);
for(i=1;i<=n;i++)
{
    scanf("%d",&l);
    scanf("%d",&w);

    if(w <= l*75/100)
    {
       sum = sum+5;
    }

    else
    {
        sum = sum +3;
    }
}
printf("%d",sum);
    return 0;
}
