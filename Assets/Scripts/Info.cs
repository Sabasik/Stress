public class Info
{
    public static float speed = 1f;
    public static string text = "";

    public string getText() {
        return text;
    }
    public float getSpeed() {
        return speed;
    }

    public void setText(string newText) {
        text = newText;
    }

    public void setSpeed(float newSpeed) {
        speed = newSpeed;
    }
}