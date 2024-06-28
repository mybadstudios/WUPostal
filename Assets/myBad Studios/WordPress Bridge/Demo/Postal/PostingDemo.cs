using UnityEngine;
using UnityEngine.UI;
using MBS;

public class PostingDemo : MonoBehaviour
{
    [Header("Make new post")]
    public InputField Title;
    public InputField Content, Summary;
    public Dropdown PostType;
    public Button MakePostButton;

    [Header("Specify post thumbnail")]
    public InputField PostId;
    public InputField ThumbnailId;
    public Button SpecifyButton;

    [Header("Upload post thumbnail")]
    public InputField Post;
    public InputField Filename;
    public Dropdown Extension;
    public Image Thumbnail;
    public Button UploadButton;

    [Header("Panels")]
    public GameObject Panel1;
    public GameObject Panel2;

    // Start is called before the first frame update
    void Start()
    {
        WULogin.OnLoggedIn += RunDemo;
        Panel1.SetActive(false);
        Panel2.SetActive(false);
    }

    void RunDemo(CML _)
    {
        Panel1.SetActive(true);
        Panel2.SetActive(true);
    }

    #region Make new entry in the POST table
    public void SubmitPost()
    {
        if (Title.text == string.Empty && Content.text == string.Empty && Summary.text == string.Empty)
        {
            Debug.LogError("Trying to post nothing. Ideally complete all three fields but complete at least one");
            return;
        }

        //add any of the other fields you might want to manually set...
        CMLData optional = new CMLData();
        optional.Set("post_status", "publish"); //defaults to "draft" if not set

        MakePostButton.interactable = false;
        WUPostal.MakePost(Title.text, Content.text, Summary.text, PostType.options[PostType.value].text, optional, AfterPosting, IfPostingFailed);
    }

    void AfterPosting(CML response)
    {
        MakePostButton.interactable = true;
        Debug.LogWarning($"The post you just made has ID { response[0].Int("pid") }");
    }

    void IfPostingFailed(CMLData error)
    {
        Debug.LogError(error.String("message"));
        MakePostButton.interactable = true;
    }
    #endregion

    #region Specify an existing image as the thumbnail
    public void SpecifyThumbnail()
    {
        if (!int.TryParse(PostId.text, out int pid))
        {
            Debug.LogError("Invalid Post ID. Check phpmyadmin for the ID of a field with post_type 'post'");
            return;
        }
        if (!int.TryParse(ThumbnailId.text, out int iid))
        {
            Debug.LogError("Invalid Attachment ID. Check phpmyadmin for the ID of a field with post_type 'attachment' and mime type jpg or png");
            return;
        }
        if (pid == 0 || iid == 0)
        {
            Debug.LogError("Invalid post values supplied!");
            return;
        }

        //add any of the other fields you might want to manually set...
        SpecifyButton.interactable = false;
        WUPostal.SpecifyFeaturedImage(pid, iid, AfterSpecifying, IfSpecifyingFailed);
    }

    void AfterSpecifying(CML response)
    {
        SpecifyButton.interactable = true;
        Debug.LogWarning($"The post you just made has ID { response[0].Int("pid") }");
    }

    void IfSpecifyingFailed(CMLData error)
    {
        Debug.LogError(error.String("message"));
        SpecifyButton.interactable = true;
    }
    #endregion

    #region Upload an image as an attachment to a post
    public void UploadPostThumbnail()
    {
        Texture2D tn = Thumbnail.sprite.texture;
        string id = Post.text.Trim();
        string filename = Filename.text.Trim();
        string ext = Extension.options[Extension.value].text;
        var extension = (WUPostal.EImageFormat)System.Enum.Parse(typeof(WUPostal.EImageFormat), ext);

        if (!int.TryParse(id, out int post_id))
        {
            Debug.LogError("A numeric post value is required");
            return;
        }
        if (filename == string.Empty)
        {
            Debug.LogError("A file cannot be uploaded without a filename...");
            return;
        }

        UploadButton.interactable = false;
        WUPostal.UploadFeaturedImage(post_id,tn, filename, extension, -1, AfterUpload, OnUploadFailed);
    }

    void AfterUpload(CML response)
    {
        UploadButton.interactable = true;
        Debug.LogWarning($"The image has ID { response[0].Int("attachment_id") }");
    }

    void OnUploadFailed(CMLData error)
    {
        Debug.LogError(error.String("message"));
        UploadButton.interactable = true;
    }

    #endregion
}
