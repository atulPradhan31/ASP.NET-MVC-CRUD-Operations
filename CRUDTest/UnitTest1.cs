namespace CRUDTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            MathModel model = new MathModel();
            int input1 = 5;
            int input2 = 10;
            int expected = 15;

            //Act
            int actual = model.Add(input1, input2);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}