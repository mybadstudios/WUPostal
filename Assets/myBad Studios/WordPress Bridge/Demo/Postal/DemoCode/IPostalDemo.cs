/// <summary>
/// This interface was created solely to gather the function names of 
/// the various demo functions together for ease of reference.
/// </summary>
public interface IPostalDemo {
    //======== SEARCH BY BASIC ELEMENTS ======
    void DoASearch();
    void FindPostByID(int pid);
    void FindPageByID(int pid);
    void FindPostBySlug();
    void FindChildViaSlugAndChild();
    void FindChildViaParentID();
    void ShowOnlyTopLevelPages();
    void PostsWithParenstInArray();
    void ShowSpecificPosts();
    void DisplayAllPagesExceptSelected();

    //======== SPECIFYING POST AUTHOR ========
    void OneAuthorByNiceName(string name);
    void OneAuthorByID(int id);
    void MultipleSpecificAuthorsByID();
    void AllExcludingOneAuthor();
    void AllExcludingSpecificAuthors();
        
     //   ======== SPECIFYING POST CATEGORY ========
    void OneCategoryAndChildrenById();
    void OneCategoryAndChildrenBySlug();
    void OneCategoryById();
    void MultipleCategories();
    void MultipleCategoriesBySlug();
    void HasAllCategoriesBySlug();
    void ExcludeCategories();
    void HasMultipleCategories();
    void OneOfSeveralCategories();

    //======== DATE EXAMPLE ================
    void From9To5Weekdays();

    //======== SPECIFYING POST TAGS ========
    void OneTagByID();
    void OneTagBySlug();
    void TagsFromSlugList();
    void MultipleTagsBySlug();
    void MultipleTagsById();
    void TagsFromIdList();
    void ExcludesPostsWithTheseIds();

    //======== SELECT BY TAXONOMY ========
    void FindBobUnderPeople();
    void DoNestedTaxonomySearch();
    void DoSelectiveTaxonomySearch();
    void DoMultipleTaxonomySearch();

    // ======= DEALING WITH PASSWORDS =====
    void FindOnlyPasswordProtectedPosts();
    void FindOnlyPublicPosts(); // no passwords
    void FindAllPostsWithASpecficPassword();

}
