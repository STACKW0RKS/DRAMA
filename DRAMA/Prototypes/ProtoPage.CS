using Microsoft.Playwright;

namespace DRAMA.Prototypes
{
    /// <summary>
    ///     Architecturally, this class is intended to be the base class of all page object models.
    ///     <br/>
    ///     This class exposes the Page property, which holds a reference to the Playwright page object used during initialisation of objects of derived classes.
    /// </summary>
    public abstract class ProtoPage
    {
        protected ProtoPage(IPage page) => Page = page;

        public IPage Page { get; init; }
    }
}