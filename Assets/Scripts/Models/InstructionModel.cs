using System;

[Serializable]
public class InstructionModel
{
    public string INSTANCE_NUMBER;
    public string OPERATION_SEQ_NUM;
    public string OPERATION_DESCRIPTION;
    public string LONG_DESCRIPTION;
    public string INSTRUMENTS;
    public string OPERATION_TIME;

    public InstructionModel(string INSTANCE_NUMBER, string OPERATION_SEQ_NUM, string OPERATION_DESCRIPTION, string LONG_DESCRIPTION, string INSTRUMENTS, string OPERATION_TIME)
    {
        this.INSTANCE_NUMBER = INSTANCE_NUMBER;
        this.OPERATION_SEQ_NUM = OPERATION_SEQ_NUM;
        this.OPERATION_DESCRIPTION = OPERATION_DESCRIPTION;
        this.LONG_DESCRIPTION = LONG_DESCRIPTION;
        this.INSTRUMENTS = INSTRUMENTS;
        this.OPERATION_TIME = OPERATION_TIME;
    }

    public string getINSTANCE_NUMBER()
    {
        return this.INSTANCE_NUMBER;
    }

    public string getOPERATION_SEQ_NUM()
    {
        return this.OPERATION_SEQ_NUM;
    }

    public string getOPERATION_DESCRIPTION()
    {
        return this.OPERATION_DESCRIPTION;
    }

    public string getLONG_DESCRIPTION()
    {
        return this.LONG_DESCRIPTION;
    }

    public string getINSTRUMENTS()
    {
        return this.INSTRUMENTS;
    }

    public string getOPERATION_TIME()
    {
        return this.OPERATION_TIME;
    }
}


